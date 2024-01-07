import { Request, Response, NextFunction } from "express";
import jwt, { JwtPayload } from "jsonwebtoken";
import UserFile from "../models/file";
import Shared from "../models/shared";

const isFileExists = async (
  req: Request,
  res: Response,
  next: NextFunction
) => {
  try {
    let fileKey;

    if (req.method === "GET") {
      fileKey = req.params.fileKey;
    } else {
      fileKey = req.body.fileKey;
    }

    if (!fileKey) {
      console.log("File key not found");
      return res.status(404).send("File Key Not Found");
    }

    const file = await UserFile.findOne({ fileKey });

    if (!file) {
      console.log("File not found");
      return res.status(404).send("File nout found");
    }

    const sharedFile = await Shared.findOne({
      targetUserId: req.user?._id,
      targetObjectId: file._id,
    });

    const ownerFile = await UserFile.findOne({
      fileKey,
      ownerId: req.user?._id,
    });

    if (!ownerFile && !sharedFile) {
      console.log("File not found");
      return res.status(404).send("File nout found");
    }
    next();
  } catch (e) {
    console.log(e);
  }
};

export { isFileExists };
