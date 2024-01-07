import { Button } from "@material-tailwind/react";
import axios from "axios";
import { useContext, useState } from "react";
import { AuthContext } from "../../context/authContext";

const mime = require("mime")

export default function ClientFileUpload() {
    const { _id, name, mail } = useContext(AuthContext);

    const [file, setFile] = useState<File | null>(null)
    const [loading, setLoading] = useState<boolean>(false)

    const handleFileSelected = (e: React.ChangeEvent<HTMLInputElement>): void => {
        const files = Array.from(e.target.files ? e.target.files : [])

        if (files && files.length > 0) {
            setFile(files[0])
        }
        else {
            alert("Please select a proper file")
        }
    }

    async function handleUploadFile(e: any) {

        // If file selected: load file to AWS
        if (file) {
            if (file.size > (1024 * 1024 * 50)) {
                alert("Please Select maxium 50MB File")
                return;
            }
            setLoading(true)
            try {
                // Send file to AWS
                var fileKey = `${_id}/${file.name}`;
                var fileType = mime.getType(file.name) as string

                let { data: { url } } = await axios.get(`${process.env.REACT_APP_API_ROUTE}/file/signPrivateFile/${encodeURIComponent(fileKey)}/${encodeURIComponent(fileType)}`,
                    { withCredentials: true });

                await axios.put(url, file, {
                    headers: {
                        "Content-type": fileType,
                        "Access-Control-Allow-Origin": "*",
                    },
                });

                const res = await axios.post(`${process.env.REACT_APP_API_ROUTE}/dbActions/recordFileUpload`, {
                    fileName: file.name, fileKey, fileType, fileSize: file.size, ownerId: _id
                }, {
                    headers: {
                        "Content-Type": "application/json",
                    }, withCredentials: true
                })

                if (res.status === 200) {
                    alert("Upload Finished")
                }
            }
            catch (err) {
                alert(err)
            }
        }
        else {
            alert("Please Select File")

        }

        setLoading(false)
    }
    return (
        <div className="">
            <label htmlFor="dropzone-file" className="flex flex-col min-h-64 items-center w-full p-5 mx-auto mt-2 text-center  justify-center border-2 border-dashed cursor-pointer bg-gray-900 border-gray-700 rounded-xl">
                <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" strokeWidth="1.5" stroke="currentColor" className="w-8 h-8 text-gray-400">
                    <path strokeLinecap="round" strokeLinejoin="round" d="M12 16.5V9.75m0 0l3 3m-3-3l-3 3M6.75 19.5a4.5 4.5 0 01-1.41-8.775 5.25 5.25 0 0110.233-2.33 3 3 0 013.758 3.848A3.752 3.752 0 0118 19.5H6.75z" />
                </svg>
                <div>
                    <h2 className="mt-1 font-medium tracking-wide text-gray-200 text-sm">Select File</h2>

                    <p className="mt-2 text-xs tracking-wide text-gray-400">Click and select file .zip, .png, .jpeg, .pdf ... </p>
                </div>
                <input id="dropzone-file" type="file" className="hidden" multiple={false} onChange={handleFileSelected} />
            </label>
            <Button color="white" loading={loading} onClick={handleUploadFile} variant="filled" className="mt-5">Upload File</Button>
        </div>
    )
}