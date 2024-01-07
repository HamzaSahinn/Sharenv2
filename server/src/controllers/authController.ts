import { Request, Response } from "express";
import User from "../models/user";
import { generateToken, clearToken, generateRefreshToken } from "../utils/auth";
import {
  BadRequestError,
  AuthenticationError,
} from "../middleware/errorMiddleware";
import asyncHandler from "express-async-handler";

const registerUser = asyncHandler(async (req: Request, res: Response) => {
  const { name, email, password } = req.body;
  const userExists = await User.findOne({ email });

  if (userExists) {
    res.status(409).json({ message: "The email already exists" });
  }

  const user = await User.create({
    name,
    email,
    password,
  });

  if (user) {
    const token = generateToken({
      userId: user._id.toString(),
      userEmail: user.email,
      userName: user.name,
    });

    res
      .status(200)
      .cookie("jwt", token, {
        httpOnly: false,
        maxAge: 3 * 24 * 60 * 60 * 1000,
      })
      .json({
        id: user._id,
        name: user.name,
        email: user.email,
        accessToken: token,
      });
  } else {
    throw new BadRequestError("An error occurred in registering the user");
  }
});

const authenticateUser = asyncHandler(async (req: Request, res: Response) => {
  const { email, password } = req.body;
  const user = await User.findOne({ email });

  if (user && (await user.comparePassword(password))) {
    const token = generateToken({
      userId: user._id.toString(),
      userEmail: user.email,
      userName: user.name,
    });

    res
      .cookie("jwt", token, {
        httpOnly: false,
        maxAge: 3 * 24 * 60 * 60 * 1000,
      })
      .status(200)
      .json({
        id: user._id,
        name: user.name,
        email: user.email,
        accessToken: token,
      });
  } else {
    throw new AuthenticationError("User not found / password incorrect");
  }
});

const logoutUser = asyncHandler(async (req: Request, res: Response) => {
  clearToken(res);
  res.status(200).json({ message: "Successfully logged out" });
});

export { registerUser, authenticateUser, logoutUser };
