declare global {
  namespace NodeJS {
    interface ProcessEnv {
      MONGO_CONN_URL: string;
      NODE_ENV: "development" | "production";
      PORT: string;
      JWT_SECRET: string;
      S3_BUCKET_NAME: string;
      S3_BUCKET_KEY: string;
      S3_BUCKET_SECRET: string;
      S3_BUCKET_REGION: string;
    }
  }
}

export {};
