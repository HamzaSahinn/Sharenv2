export type UserSession = {
  name: string;
  id: string;
  email: string;
};

export type UserFile = {
  _id: string;
  name: string;
  fileKey: string;
  fileType: string;
  fileSize: number;
  ownerId: string;
  createdAt: string;
  updatedAt: string;
};

export type SharedFiles = {
  ownerName: string;
  sharedAt: string;
  ownerMail: string;
  fileName: string;
  fileKey: string;
  fileType: string;
  fileSize: number;
};

export type User = {
  _id: string;
  name: string;
  email: string;
};
