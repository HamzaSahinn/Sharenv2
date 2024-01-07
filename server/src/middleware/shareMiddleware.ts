import { Request, Response, NextFunction } from "express";
import jwt, { JwtPayload } from "jsonwebtoken";
import UserFile from "../models/file";
import User from "../models/user";

const isShareFileDataCorrect = async (
  req: Request,
  res: Response,
  next: NextFunction
) => {
  try {
    const fileId = req.body.fileId;
    const targetuserId = req.body.targetUserId;

    if (!fileId || !targetuserId) {
      console.log("Data not valid");
      return res.status(404).send("Data not valid");
    }

    const file = await UserFile.findOne({
      _id: fileId,
      ownerId: req.user?._id,
    });

    const user = await User.findOne({ _id: targetuserId });

    if (!file || !user) {
      console.log("User or File Nout Found");
      return res.status(404).send("User or File Nout Found");
    }

    next();
  } catch (e) {
    console.log(e);
  }
};

export { isShareFileDataCorrect };
