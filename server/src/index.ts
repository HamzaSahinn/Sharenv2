import express from "express";
import { Application } from "express";
import dotenv from "dotenv";
import authRouter from "./routes/authRouter";
import connectMongoDB from "./connections/mongodb";
import filesRouter from "./routes/filesRouter";
import dbRouter from "./routes/dbRouter";
import shareRouter from "./routes/shareRouter";

var cookieParser = require("cookie-parser");
const cors = require("cors");

// Create the express app and  import the type of app from express;
const app: Application = express();

//configure env;
dotenv.config();
// Parser
app.use(express.json());
app.use(
  express.urlencoded({
    extended: true,
  })
);

app.use(
  cors({
    origin: ["http://localhost:3000"],
    methods: ["GET", "POST", "PUT", "DELETE"],
    credentials: true,
  })
);
app.use(cookieParser());

interface UserBasicInfo {
  _id: string;
  email: string;
  name: string;
}

declare global {
  namespace Express {
    interface Request {
      user?: UserBasicInfo | null;
    }
  }
}

app.get("/", (req, res) => {
  res.send("<h1>Welcome To Sharenv Backend </h1>");
});

app.use("/auth", authRouter);

app.use("/file", filesRouter);

app.use("/dbActions", dbRouter);

app.use("/share", shareRouter);

const start = async (): Promise<void> => {
  try {
    console.log(`⚡️[server]: Server is trying to initiate db connection`);
    await connectMongoDB();
    app.listen(process.env.PORT, () => {
      console.log(
        `⚡️[server]: Server is running at http://localhost:${process.env.PORT}`
      );
    });
  } catch (error) {
    console.error(error);
    process.exit(1);
  }
};

start();
