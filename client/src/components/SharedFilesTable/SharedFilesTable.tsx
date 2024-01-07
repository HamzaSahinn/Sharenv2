import { Button, IconButton, Popover, PopoverContent, PopoverHandler } from "@material-tailwind/react";
import { useContext, useEffect, useState } from "react";
import { useCookies } from "react-cookie";
import { AuthContext } from "../../context/authContext";
import axios from "axios";
import { SharedFiles, UserFile } from "../../types";
import { DocumentTextIcon, PhotoIcon, EllipsisVerticalIcon, CloudArrowDownIcon, TrashIcon, ShareIcon } from "@heroicons/react/24/outline";
import { dateString2HumanReadableDate, parseFileSize } from "../../utils/convertions";
import { useNavigate } from "react-router-dom";



export default function SharedFilesTable() {
    const { _id, name, mail } = useContext(AuthContext);
    const [userFiles, setUserFiles] = useState<SharedFiles[]>()

    const navigate = useNavigate()

    useEffect(() => {
        if (_id) {
            axios.get(`${process.env.REACT_APP_API_ROUTE}/dbActions/sharedFiles/${_id}`, { withCredentials: true }).then(res => { if (res.status === 200) { setUserFiles(res.data) } })
        }
    }, [_id])

    const handleDownload = async (fileKey: string) => {
        const res = await axios.get(`${process.env.REACT_APP_API_ROUTE}/file/getPrivateFileLink/${encodeURIComponent(fileKey)}`, { withCredentials: true })

        if (res.status === 200) {
            const { url } = await res.data
            const link = document.createElement("a",);
            link.href = url;
            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);
        }
        else {
            alert(res.status + " Error on download")
        }
    }

    const handleDelete = async (fileKey: string) => {
        const res = await axios.delete(`${process.env.REACT_APP_API_ROUTE}/file/deletePrivateFile/${encodeURIComponent(fileKey)}`, { withCredentials: true })

        if (res.status === 200) {
            alert("File Removed")
            navigate(0)
        }
        else {
            alert("File is not deleted")
        }
    }

    return (
        <section className="container px-4 mx-auto">

            <div className="flex items-center justify-start gap-x-3">
                <h2 className="text-lg font-medium text-white text-left w-auto">Files Uploaded</h2>
                <span className="px-3 py-1 text-xs bg-gray-800 text-blue-400 rounded-full">{userFiles?.length} files</span>
            </div>

            <div className="flex flex-col mt-6">
                <div className="-mx-4 -my-2 overflow-x-auto sm:-mx-6 lg:-mx-8">
                    <div className="inline-block min-w-full py-2 align-middle md:px-6 lg:px-8">
                        <div className="overflow-hidden border border-gray-700 md:rounded-lg">
                            <table className="min-w-full divide-y divide-gray-700">
                                <thead className="bg-gray-800">
                                    <tr>
                                        <th scope="col" className="py-3.5 px-4 text-sm font-normal text-left  text-gray-500 dark:text-gray-400">
                                            <div className="flex items-center gap-x-3">
                                                <span>File Name</span>
                                            </div>
                                        </th>

                                        <th scope="col" className="px-4 py-3.5 text-sm font-normal text-left rtl:text-right text-gray-500 dark:text-gray-400">
                                            File Owner
                                        </th>

                                        <th scope="col" className="px-4 py-3.5 text-sm font-normal text-left rtl:text-right text-gray-500 dark:text-gray-400">
                                            Date Shared
                                        </th>

                                        <th scope="col" className="relative py-3.5 px-4">
                                            <span className="sr-only">Edit</span>
                                        </th>
                                    </tr>
                                </thead>
                                <tbody className="bg-gray-900 divide-y divide-gray-200 dark:divide-gray-700">
                                    {userFiles?.map((file, index) => {
                                        return (
                                            <tr>
                                                <td className="px-4 py-4 text-sm font-medium text-gray-700 whitespace-nowrap">
                                                    <div className="inline-flex items-center gap-x-3">
                                                        <div className="flex items-center gap-x-2">
                                                            <div className="flex items-center justify-center w-8 h-8 text-blue-500 rounded-full bg-gray-800">
                                                                {file.fileType.includes("image") ? <PhotoIcon className="w-6 h-6" /> : <DocumentTextIcon className="w-6 h-6" />}
                                                            </div>

                                                            <div>
                                                                <h2 className="font-normal text-sm text-white ">{file.fileName}</h2>
                                                                <p className="text-xs font-normal text-gray-500 dark:text-gray-400">{parseFileSize(file.fileSize)}</p>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td className="px-4 py-4 text-sm text-gray-500 dark:text-gray-300 whitespace-nowrap">{file.ownerName}</td>

                                                <td className="px-4 py-4 text-sm text-gray-500 dark:text-gray-300 whitespace-nowrap">{dateString2HumanReadableDate(file.sharedAt)}</td>

                                                <td className="px-4 py-4 text-sm whitespace-nowrap">

                                                    <Popover placement="bottom">
                                                        <PopoverHandler>
                                                            <IconButton size="sm">
                                                                <EllipsisVerticalIcon className="w-7 h-7" />
                                                            </IconButton>
                                                        </PopoverHandler>
                                                        <PopoverContent className="z-50 bg-gray-800 text-white">
                                                            <div className="flex flex-row gap-3">
                                                                <IconButton size="sm" className="text-blue-600 hover:text-blue-700 hover:bg-gray-700" onClick={() => handleDownload(file.fileKey)}>
                                                                    <CloudArrowDownIcon className="w-6 h-6" />
                                                                </IconButton>

                                                                <IconButton size="sm" className="text-red-600 hover:text-red-700 hover:bg-gray-700" onClick={() => handleDelete(file.fileKey)}>
                                                                    <TrashIcon className="w-6 h-6" />
                                                                </IconButton>

                                                                <IconButton size="sm" className="text-green-600 hover:text-green-700 hover:bg-gray-700">
                                                                    <ShareIcon className="w-6 h-6" />
                                                                </IconButton>
                                                            </div>
                                                        </PopoverContent>
                                                    </Popover>

                                                </td>
                                            </tr>
                                        )
                                    })}

                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    )
}