import Shared from "../models/shared";

const getSharedFilesObjects = async (targetUserId: string) => {
  const sharedFiles = await Shared.aggregate([
    {
      $match: { targetUserId: targetUserId },
    },
    { $addFields: { targetObjectIdOID: { $toObjectId: "$targetObjectId" } } },
    {
      $lookup: {
        from: "userfiles",
        localField: "targetObjectIdOID",
        foreignField: "_id",
        as: "files",
      },
    },
    {
      $unwind: "$files",
    },
    { $addFields: { ownerIdOID: { $toObjectId: "$ownerId" } } },
    {
      $lookup: {
        from: "users",
        localField: "ownerIdOID",
        foreignField: "_id",
        as: "owner",
      },
    },
    {
      $unwind: "$owner",
    },
    {
      $project: {
        sharedAt: "$createdAt",
        fileName: "$files.name",
        fileKey: "$files.fileKey",
        fileType: "$files.fileType",
        fileSize: "$files.fileSize",
        ownerMail: "$owner.email",
        ownerName: "$owner.name",
      },
    },
  ]);

  return sharedFiles;
};
export { getSharedFilesObjects };
