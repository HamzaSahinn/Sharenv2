declare global {
    namespace NodeJS {
      interface ProcessEnv {
        MONGO_CONN_URL: string;
        NODE_ENV: 'development' | 'production';
        PORT: string;
        JWT_SECRET:string;
      }
    }
  }

  export {}