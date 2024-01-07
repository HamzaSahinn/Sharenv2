import mongoose, { Schema } from "mongoose";
const bcrypt = require("bcrypt");

export interface IShared extends Document {
  targetObjectId: string;
  targetUserId: string;
  ownerId: string;
  sharedType: "file" | "trip" | "list";
}

const sharedSchema = new Schema<IShared>(
  {
    targetObjectId: {
      type: String,
      required: true,
    },
    targetUserId: {
      type: String,
      required: true,
    },
    ownerId: {
      type: String,
      required: true,
    },
    sharedType: {
      enum: ["file", "trip", "list"],
    },
  },
  { timestamps: true }
);

const Shared = mongoose.model("Shared", sharedSchema);

export default Shared;
