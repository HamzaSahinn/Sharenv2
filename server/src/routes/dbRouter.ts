import express from "express";
import { authenticate } from "../middleware/authMiddleware";
import {
  getSharedFiles,
  getUserFiles,
  getUsers,
  recordFile,
} from "../controllers/dbController";

const dbRouter = express.Router();

dbRouter.post("/recordFileUpload/", [authenticate, recordFile]);
dbRouter.get("/userFiles/:id", [authenticate, getUserFiles]);
dbRouter.get("/users/:query", [authenticate, getUsers]);
dbRouter.get("/sharedFiles/:targetUserId", [authenticate, getSharedFiles]);
export default dbRouter;
