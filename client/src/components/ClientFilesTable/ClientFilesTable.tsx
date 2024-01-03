import { Button, IconButton } from "@material-tailwind/react";

export default function ClientFilesTable() {
    return (
        <section className="container px-4 mx-auto">

            <div className="flex items-center justify-start gap-x-3">
                <h2 className="text-lg font-medium text-white text-left w-auto">Files Uploaded</h2>
                <span className="px-3 py-1 text-xs bg-gray-800 text-blue-400 rounded-full">12 files</span>
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
                                                <span>File name</span>
                                            </div>
                                        </th>

                                        <th scope="col" className="px-12 py-3.5 text-sm font-normal text-left text-gray-500 dark:text-gray-400">
                                            File size
                                        </th>

                                        <th scope="col" className="px-4 py-3.5 text-sm font-normal text-left rtl:text-right text-gray-500 dark:text-gray-400">
                                            Date uploaded
                                        </th>

                                        <th scope="col" className="px-4 py-3.5 text-sm font-normal text-left rtl:text-right text-gray-500 dark:text-gray-400">
                                            Last updated
                                        </th>

                                        <th scope="col" className="px-4 py-3.5 text-sm font-normal text-left rtl:text-right text-gray-500 dark:text-gray-400">
                                            Uploaded by
                                        </th>

                                        <th scope="col" className="relative py-3.5 px-4">
                                            <span className="sr-only">Edit</span>
                                        </th>
                                    </tr>
                                </thead>
                                <tbody className="bg-gray-900 divide-y divide-gray-200 dark:divide-gray-700">
                                    <tr>
                                        <td className="px-4 py-4 text-sm font-medium text-gray-700 whitespace-nowrap">
                                            <div className="inline-flex items-center gap-x-3">
                                                <div className="flex items-center gap-x-2">
                                                    <div className="flex items-center justify-center w-8 h-8 text-blue-500 rounded-full bg-gray-800">
                                                        <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" className="w-5 h-5">
                                                            <path stroke-linecap="round" stroke-linejoin="round" d="M19.5 14.25v-2.625a3.375 3.375 0 00-3.375-3.375h-1.5A1.125 1.125 0 0113.5 7.125v-1.5a3.375 3.375 0 00-3.375-3.375H8.25m2.25 0H5.625c-.621 0-1.125.504-1.125 1.125v17.25c0 .621.504 1.125 1.125 1.125h12.75c.621 0 1.125-.504 1.125-1.125V11.25a9 9 0 00-9-9z" />
                                                        </svg>
                                                    </div>

                                                    <div>
                                                        <h2 className="font-normal text-sm text-white ">Tech requirements.pdf</h2>
                                                        <p className="text-xs font-normal text-gray-500 dark:text-gray-400">200 KB</p>
                                                    </div>
                                                </div>
                                            </div>
                                        </td>
                                        <td className="px-12 py-4 text-sm font-normal text-gray-700 whitespace-nowrap">
                                            200 KB
                                        </td>
                                        <td className="px-4 py-4 text-sm text-gray-500 dark:text-gray-300 whitespace-nowrap">Jan 4, 2022</td>
                                        <td className="px-4 py-4 text-sm text-gray-500 dark:text-gray-300 whitespace-nowrap">Jan 4, 2022</td>
                                        <td className="px-4 py-4 text-sm text-gray-500 dark:text-gray-300 whitespace-nowrap">Lana Steiner</td>
                                        <td className="px-4 py-4 text-sm whitespace-nowrap">
                                            <IconButton size="sm">
                                                <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" className="w-6 h-6">
                                                    <path stroke-linecap="round" stroke-linejoin="round" d="M12 6.75a.75.75 0 110-1.5.75.75 0 010 1.5zM12 12.75a.75.75 0 110-1.5.75.75 0 010 1.5zM12 18.75a.75.75 0 110-1.5.75.75 0 010 1.5z" />
                                                </svg>
                                            </IconButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    )
}