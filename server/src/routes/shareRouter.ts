import express from "express";
import { authenticate } from "../middleware/authMiddleware";
import { isShareFileDataCorrect } from "../middleware/shareMiddleware";
import { shareFile } from "../controllers/shareController";

const shareRouter = express.Router();

shareRouter.post("/file", [authenticate, isShareFileDataCorrect, shareFile]);

shareRouter.post("/trip", [authenticate]);

shareRouter.post("/list", [authenticate]);

export default shareRouter;
