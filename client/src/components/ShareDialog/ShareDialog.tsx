import { Dialog, DialogHeader, DialogBody, DialogFooter, Button, Input } from "@material-tailwind/react";
import { Dispatch, useEffect, useState } from "react";
import { MagnifyingGlassIcon } from "@heroicons/react/24/outline";
import axios from "axios";
import { User } from "../../types";

export default function ShareDialog({ isOpen, setOpen, fileId }: {
    isOpen: boolean, setOpen: Dispatch<React.SetStateAction<boolean>>, fileId: string
}) {

    const [searchQuery, setSearchQuery] = useState<string>("")
    const [searchResult, setSearchResult] = useState<User[]>()
    const [selectedUser, setSelectedUser] = useState<User>()
    const [loading, setLoading] = useState<boolean>(false)

    const toggleOpen = () => {
        setOpen(prev => !prev)
    }

    const onQueryChange = async (str: string | null) => {
        if (str === "") return;

        if (str) {
            let res = await axios.get(`${process.env.REACT_APP_API_ROUTE}/dbActions/users/${encodeURIComponent(searchQuery)}`, { withCredentials: true })
            setSearchResult(res.data)
        }
        else {
        }
    };

    const handleSelection = (selected: User) => {
        setSelectedUser(selected)
        setSearchResult([selected])
    }

    const handleShare = async () => {
        setLoading(true)
        await axios.post(`${process.env.REACT_APP_API_ROUTE}/share/file`, { fileId, targetUserId: selectedUser?._id }, { withCredentials: true })
        toggleOpen()
        setLoading(false)
    }

    useEffect(() => {
        const timeOutId = setTimeout(() => onQueryChange(searchQuery), 600);
        return () => clearTimeout(timeOutId);
    }, [searchQuery]);
    return (
        <>
            <Dialog
                open={isOpen}
                handler={toggleOpen}
                animate={{
                    mount: { scale: 1, y: 0 },
                    unmount: { scale: 0.9, y: -100 },
                }}
            >
                <DialogHeader >Choose User for Sharing</DialogHeader>
                <DialogBody>
                    <p className="mb-5">You can search user by name from below field</p>
                    <div className="relative flex items-center mt-4 md:mt-0">
                        <span className="absolute">
                            <MagnifyingGlassIcon className="w-6 h-6  mx-3 text-gray-400" />
                        </span>

                        <input onChange={(e) => setSearchQuery(e.target.value)}
                            type="text" placeholder="Search" className="border-2 border-blue-500 block w-full py-1.5 pr-5 text-gray-700 bg-white rounded-lg md:w-80 placeholder-gray-400/70 pl-11 rtl:pr-11 rtl:pl-5 focus:border-blue-400 focus:ring-blue-300 focus:outline-none focus:ring focus:ring-opacity-40" />
                    </div>

                    <div className="w-full flex flex-col divide-y my-5 border border-blue-500 p-4 rounded-md gap-4 min-h-64">
                        {searchResult?.map((element, index) => {
                            return (
                                <div key={element._id} className="flex flex-col w-full hover:bg-gray-200 p-5 rounded-md" role="button" onClick={() => { handleSelection(element) }}>
                                    <span className="font-semibold">{element.name}</span>
                                    <span className="italic">{element.email}</span>
                                </div>
                            )
                        })}
                    </div>
                </DialogBody>

                <DialogFooter>
                    <Button variant="gradient" color="blue-gray" onClick={handleShare} loading={loading}>
                        <span>Share</span>
                    </Button>
                </DialogFooter>
            </Dialog>
        </>
    )
}