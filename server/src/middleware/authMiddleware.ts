import { Request, Response, NextFunction } from "express";
import jwt, { JwtPayload } from "jsonwebtoken";
import User from "../models/user";
import asyncHandler from "express-async-handler";
import { AuthenticationError } from "./errorMiddleware";

const authenticate = (req: Request, res: Response, next: NextFunction) => {
  try {
    let token = req.cookies.jwt;

    if (!token) {
      throw new AuthenticationError("Token not found");
    }

    const jwtSecret = process.env.JWT_SECRET || "";
    const decoded = jwt.verify(token, jwtSecret) as JwtPayload;

    if (!decoded || !decoded.userId || !decoded.userEmail) {
      throw new AuthenticationError("User not found");
    }

    const { userId, userEmail, roles } = decoded;

    req.user = { _id: userId, email: userEmail, roles };
    next();
  } catch (e) {
    throw new AuthenticationError("Invalid token");
  }
};



export { authenticate };