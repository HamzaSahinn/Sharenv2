import asyncHandler from "express-async-handler";
import { Request, Response } from "express";
import S3 from "aws-sdk/clients/s3";
import {
  DeleteObjectsCommand,
  GetObjectCommand,
  S3Client,
} from "@aws-sdk/client-s3";
import { getSignedUrl } from "@aws-sdk/s3-request-presigner";
import UserFile from "../models/file";

const signPrivateFile = asyncHandler(async (req: Request, res: Response) => {
  let { fileKey, fileType } = req.params;

  if (!fileKey || !fileType) {
    res.status(404).send("File Type and File Key Required");
  } else {
    try {
      const client_s3 = new S3({
        region: process.env.S3_BUCKET_REGION,
        accessKeyId: process.env.S3_BUCKET_KEY,
        secretAccessKey: process.env.S3_BUCKET_SECRET,
        signatureVersion: "v4",
      });

      const fileParams = {
        Bucket: process.env.S3_BUCKET_NAME,
        Key: fileKey,
        Expires: 600,
        ContentType: fileType,
      };

      const url = await client_s3.getSignedUrlPromise("putObject", fileParams);

      res.status(200).json({ url });
    } catch (err) {
      console.log(err);
      res.status(500).send(err);
    }
  }
});

const getPrivateFileLink = asyncHandler(async (req: Request, res: Response) => {
  let { fileKey } = req.params;

  if (!fileKey) {
    res.status(404).send("File Type and File Key Required");
  } else {
    try {
      const s3Client = new S3Client({
        region: process.env.S3_BUCKET_REGION as string,
        credentials: {
          accessKeyId: process.env.S3_BUCKET_KEY as string,
          secretAccessKey: process.env.S3_BUCKET_SECRET as string,
        },
      });

      const command = new GetObjectCommand({
        Bucket: process.env.S3_BUCKET_NAME,
        Key: fileKey,
      });

      res.status(200).json({
        url: await getSignedUrl(s3Client, command, { expiresIn: 60 }),
      });
    } catch (err) {
      console.log(err);
      res.status(500).send(err);
    }
  }
});

const deleteFile = asyncHandler(async (req: Request, res: Response) => {
  try {
    const fileKey = req.params.fileKey;

    const s3Client = new S3Client({
      region: process.env.S3_BUCKET_REGION as string,
      credentials: {
        accessKeyId: process.env.S3_BUCKET_KEY as string,
        secretAccessKey: process.env.S3_BUCKET_SECRET as string,
      },
    });

    const params = {
      Bucket: process.env.S3_BUCKET_NAME,
      Delete: {
        Objects: [{ Key: fileKey }],
      },
    };

    await s3Client.send(new DeleteObjectsCommand(params));

    await UserFile.deleteOne({ fileKey, ownerId: req.user?._id });
    res.status(200).json({ msg: "File Deleted" });
  } catch (error) {
    console.log(error);
    res.status(500).send("Server Error");
  }
});

export { signPrivateFile, getPrivateFileLink, deleteFile };
