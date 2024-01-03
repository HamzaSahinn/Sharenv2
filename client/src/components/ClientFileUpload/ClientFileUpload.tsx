import { Button } from "@material-tailwind/react";
import axios from "axios";
import { useState } from "react";
import { useAuthUser } from "react-auth-kit";
import { UserSession } from "../../types";


const mime = require("mime")

export default function ClientFileUpload() {
    const [file, setFile] = useState<File | null>(null)
    const [loading, setLoading] = useState<boolean>(false)

    const auth = useAuthUser()
    const session = auth() as UserSession

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
                alert("Lütfen 50 MB'den küçük dosya seçin")
                return;
            }
            setLoading(true)

            // Send file to AWS
            var fileKey = `${session.id}/${file.name}`;
            var fileType = mime.getType(file.name)

            let { data } = await axios.post("http://localhost:8080/file/signPrivateFile", {
                file_key: fileKey,
                type: fileType,
            });

            const uploadUrl = await data.url;

            console.log("URL upload: ", uploadUrl, "to upload file: ", file);

            await axios.put(uploadUrl, file, {
                headers: {
                    "Content-type": fileType,
                    "Access-Control-Allow-Origin": "*",
                },
            });

            const res = await fetch('http://localhost:8080/dbActions/recordFileUpload', {
                method: 'POST',
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({})
            })

            if (res.status === 200) {

            }
            else {

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
                <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" className="w-8 h-8 text-gray-400">
                    <path stroke-linecap="round" stroke-linejoin="round" d="M12 16.5V9.75m0 0l3 3m-3-3l-3 3M6.75 19.5a4.5 4.5 0 01-1.41-8.775 5.25 5.25 0 0110.233-2.33 3 3 0 013.758 3.848A3.752 3.752 0 0118 19.5H6.75z" />
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