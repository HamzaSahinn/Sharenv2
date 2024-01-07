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

    const { userId, userEmail, userName } = decoded;

    req.user = { _id: userId, email: userEmail, name: userName };
    next();
  } catch (e) {
    console.log(e);
    throw new AuthenticationError("Invalid token");
  }
};

const checkUser = (req: Request, res: Response, next: NextFunction) => {
  const token = req.cookies.jwt;

  if (token) {
    try {
      const jwtSecret = process.env.JWT_SECRET || "";
      const decoded = jwt.verify(token, jwtSecret) as JwtPayload;
      if (
        !decoded ||
        !decoded.userId ||
        !decoded.userEmail ||
        !decoded.userName
      ) {
        throw new AuthenticationError("User not found");
      }

      const { userId, userEmail, userName } = decoded;
      return res
        .status(200)
        .json({ userId, userEmail, userName, status: true });
    } catch (error) {
      //Most propably jwt exipred. User need to login
      res.json({ status: false });
    }
  } else {
    res.json({ status: false });
    next();
  }
};

export { authenticate, checkUser };
