import { Tab, TabPanel, Tabs, TabsBody, TabsHeader } from "@material-tailwind/react";
import ClientFilesTable from "../../components/ClientFilesTable/ClientFilesTable";
import ClientFileUpload from "../../components/ClientFileUpload/ClientFileUpload";
export default function FilesPage() {

    return (
        <>
            <div className="bg-gray-900 w-full p-5">
                <div className="">
                    <Tabs id="custom-animation" value="myFiles">
                        <TabsHeader>
                            <Tab key={"myFiles"} value={"myFiles"}>
                                My Files
                            </Tab>

                            <Tab key={"fileUpload"} value={"fileUpload"}>
                                File Upload
                            </Tab>
                        </TabsHeader>

                        <TabsBody
                            animate={{
                                initial: { y: 250 },
                                mount: { y: 0 },
                                unmount: { y: 250 },
                            }}
                        >
                            <TabPanel key={"myFiles"} value={"myFiles"} >
                                <ClientFilesTable />
                            </TabPanel>

                            <TabPanel key={"fileUpload"} value={"fileUpload"}>
                                <ClientFileUpload />
                            </TabPanel>
                        </TabsBody>
                    </Tabs>
                </div>

                <div className="w-full">

                </div>

            </div>
        </>
    )
}