import express from "express";
import {
  deleteFile,
  getPrivateFileLink,
  signPrivateFile,
} from "../controllers/fileController";
import { authenticate } from "../middleware/authMiddleware";
import { isFileExists } from "../middleware/fileMiddleware";

const filesRouter = express.Router();

filesRouter.get("/signPrivateFile/:fileKey/:fileType", [
  authenticate,
  signPrivateFile,
]);

filesRouter.get("/getPrivateFileLink/:fileKey", [
  authenticate,
  isFileExists,
  getPrivateFileLink,
]);

filesRouter.delete("/deletePrivateFile/:fileKey", [
  authenticate,
  isFileExists,
  deleteFile,
]);

export default filesRouter;
