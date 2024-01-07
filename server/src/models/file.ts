import mongoose, { Schema } from "mongoose";
const bcrypt = require("bcrypt");

export interface IFile extends Document {
  name: string;
  fileKey: string;
  fileType: string;
  fileSize: number;
  ownerId: string;
}

const fileSchema = new Schema<IFile>(
  {
    name: {
      type: String,
      required: true,
    },
    fileKey: {
      type: String,
      required: true,
      unique: true,
    },
    fileType: {
      type: String,
      required: true,
    },
    fileSize: {
      type: Number,
      required: true,
    },
    ownerId: {
      type: String,
      required: true,
    },
  },
  { timestamps: true }
);

const UserFile = mongoose.model("UserFile", fileSchema);

export default UserFile;
