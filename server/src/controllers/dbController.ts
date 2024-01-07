import asyncHandler from "express-async-handler";
import { Request, Response } from "express";
import UserFile from "../models/file";
import User from "../models/user";
import Shared from "../models/shared";
import { getSharedFilesObjects } from "../utils/shared";

const recordFile = asyncHandler(async (req: Request, res: Response) => {
  const { fileName, fileKey, fileType, fileSize, ownerId } = req.body;

  if (!req.user?._id || !req.user?.email) {
    res.status(404).send("User Not Found");
  } else {
    try {
      const file = await UserFile.create({
        name: fileName,
        fileKey,
        fileType,
        fileSize,
        ownerId,
      });
      res.status(200).json({
        msg: `User File Record Created with id: ${file._id.toString()}`,
      });
    } catch (error) {
      console.log(`Error in creating a new user file : ${error}`);
      res.status(500).json({ msg: error });
    }
  }
});

const getUserFiles = asyncHandler(async (req: Request, res: Response) => {
  const id = req.params.id;

  if (!id) {
    res.status(404).send("User not found");
  }
  try {
    const files = await UserFile.find({ ownerId: id });
    res.status(200).json(files);
  } catch (error) {
    console.log(error);
    res.status(500).send("Server Error");
  }
});

const getUsers = asyncHandler(async (req: Request, res: Response) => {
  try {
    const query = req.params.query;

    if (!query) throw Error("Query Not Found");

    const users = await User.find({ $text: { $search: `\'${query}\'` } })
      .sort()
      .limit(5);

    res.status(200).json(users);
  } catch (error) {
    console.log(error);
    res.status(500).send("server error");
  }
});

const getSharedFiles = asyncHandler(async (req: Request, res: Response) => {
  try {
    const targetUserId = req.params.targetUserId;

    if (!targetUserId) {
      console.log("target user not found");
      throw new Error("target user not found");
    }
    const sharedFiles = await getSharedFilesObjects(targetUserId);

    res.status(200).json(sharedFiles);
  } catch (error) {
    res.status(500).send("server error");
  }
});

export { recordFile, getUserFiles, getUsers, getSharedFiles };
