import express from "express";
import { Application } from "express";
import mongoose from "mongoose";
import cors from "cors";
import dotenv from "dotenv";
import User from "./models/user";
import authRouter from "./routes/authRouter";
import connectMongoDB from "./connections/mongodb";
const bcrypt = require('bcrypt');
var cookieParser = require('cookie-parser')

// Create the express app and  import the type of app from express;
const app: Application = express();

// Cors
app.use(cors());
//configure env;
dotenv.config();
// Parser
app.use(express.json());
app.use(
  express.urlencoded({
    extended: true,
  })
);
app.use(cookieParser());

interface UserBasicInfo {
  _id: string;
  email: string;
  roles: string[];
}

declare global {
  namespace Express {
    interface Request {
      user?: UserBasicInfo | null;
    }
  }
}

app.get("/", (req, res) => {
  res.send("<h1>Welcome To JWT Authentication </h1>");
});

app.use("/auth", authRouter);


const start = async (): Promise<void> => {
  try {
    console.log(`⚡️[server]: Server is trying to initiate db connection`);
    await connectMongoDB();
    app.listen(process.env.PORT, () => {
      console.log(`⚡️[server]: Server is running at http://localhost:${process.env.PORT}`);
    });
  } catch (error) {
    console.error(error);
    process.exit(1);
  }
};

start();