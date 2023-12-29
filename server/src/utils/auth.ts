import jwt from "jsonwebtoken";
import { Response } from "express";

interface TokenPayload {
  userId: string;
  userEmail: string;
}

const generateToken = (payload: TokenPayload) => {
  const jwtSecret = process.env.JWT_SECRET || "";
  const token = jwt.sign(payload, jwtSecret, {
    expiresIn: "1h",
  });
  return token
};

const generateRefreshToken = (payload: TokenPayload) => {
  const jwtSecret = process.env.JWT_SECRET || "";
  const token = jwt.sign(payload, jwtSecret, {
    expiresIn: "1d",
  });
  return token
}

const clearToken = (res: Response) => {
  res.cookie("jwt", "", {
    httpOnly: true,
    expires: new Date(0),
  });
};

export { generateToken, clearToken, generateRefreshToken };