export function parseFileSize(fileSizeInBytes: number) {
  if (fileSizeInBytes < 1024 * 1024)
    return (fileSizeInBytes / 1024).toFixed(2) + "KB";
  else return (fileSizeInBytes / (1024 * 1024)).toFixed(2) + "MB";
}

export function dateString2HumanReadableDate(dateString: string) {
  const date = new Date(dateString);
  return [date.getDate(), date.getMonth() + 1, date.getFullYear()].join(".");
}
