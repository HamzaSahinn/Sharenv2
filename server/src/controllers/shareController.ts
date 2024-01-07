import asyncHandler from "express-async-handler";
import { Request, Response } from "express";
import Shared from "../models/shared";

const shareFile = asyncHandler(async (req: Request, res: Response) => {
  try {
    const targetUserId = req.body.targetUserId;
    const targetObjectId = req.body.fileId;
    const ownerId = req.user?._id;

    await Shared.create({
      targetObjectId,
      targetUserId,
      ownerId,
      sharedType: "file",
    });

    res.status(200).send("File shared");
  } catch (error) {
    console.log(error);
    res.status(500).send("Server Error");
  }
});

export { shareFile };
