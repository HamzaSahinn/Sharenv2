import React, { Dispatch } from "react";
import {
    IconButton,
    Typography,
    List,
    ListItem,
    ListItemPrefix,
    ListItemSuffix,
    Chip,
    Accordion,
    AccordionHeader,
    AccordionBody,
    Alert,
    Input,
    Drawer,
    Card,
} from "@material-tailwind/react";
import {
    GlobeAsiaAustraliaIcon,
    InboxIcon,
    PowerIcon,
    ListBulletIcon
} from "@heroicons/react/24/outline";
import Logo from './../../assets/image/logoBlue.jpg'
import LogoutWrapper from "../LogoutWrapper/LogoutWrapper";
import { Link } from "react-router-dom";

export function Sidenav({ openSidenav, setOpenSidenav }: { openSidenav: boolean, setOpenSidenav: React.Dispatch<React.SetStateAction<boolean>> }) {

    return (
        <>
            <Drawer open={openSidenav} onClose={() => setOpenSidenav(false)}>
                <Card
                    color="transparent"
                    shadow={false}
                    className="h-[calc(100vh-2rem)] w-full p-4"
                >
                    <Link to={"/dashboard"} onClick={() => setOpenSidenav(false)} className="mb-2 flex items-center gap-2 p-4">
                        <img
                            src={Logo}
                            alt="brand"
                            className="h-8 w-8 rounded-lg"
                        />
                        <Typography variant="h5" color="blue-gray">
                            Sharenv
                        </Typography>
                    </Link>
                    <List>
                        <Link to={"/dashboard/files"} onClick={() => { setOpenSidenav(false) }}>
                            <ListItem>
                                <ListItemPrefix>
                                    <InboxIcon className="h-6 w-6" />
                                </ListItemPrefix>
                                My Files
                            </ListItem>
                        </Link>

                        <ListItem>
                            <ListItemPrefix>
                                <GlobeAsiaAustraliaIcon className="h-6 w-6" />
                            </ListItemPrefix>
                            Trips
                        </ListItem>

                        <ListItem>
                            <ListItemPrefix>
                                <ListBulletIcon className="h-6 w-6" />
                            </ListItemPrefix>
                            Lists
                        </ListItem>

                        <LogoutWrapper>
                            <ListItem>
                                <div className="flex flex-row w-full">
                                    <ListItemPrefix>
                                        <PowerIcon className="h-6 w-6" />
                                    </ListItemPrefix>
                                    Log Out
                                </div>
                            </ListItem>
                        </LogoutWrapper>
                    </List>
                </Card>
            </Drawer>
        </>
    );
}