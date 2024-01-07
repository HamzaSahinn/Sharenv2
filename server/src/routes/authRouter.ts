import express from "express";
import {
  registerUser,
  authenticateUser,
  logoutUser,
} from "../controllers/authController";
import { authenticate, checkUser } from "../middleware/authMiddleware";

const authRouter = express.Router();

authRouter.post("/", checkUser);
authRouter.post("/register", registerUser);
authRouter.post("/login", authenticateUser);
authRouter.get("/logout", logoutUser);

export default authRouter;
