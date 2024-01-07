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
import { Link, useNavigate } from "react-router-dom";
import { useCookies } from "react-cookie";
import axios from "axios";

export function Sidenav({ openSidenav, setOpenSidenav }: { openSidenav: boolean, setOpenSidenav: React.Dispatch<React.SetStateAction<boolean>> }) {
    const [cookies, setCookie, removeCookie] = useCookies(["jwt"]);
    const navigate = useNavigate();

    const logOut = async () => {
        removeCookie("jwt");
        await axios.get(`${process.env.REACT_APP_API_ROUTE}/auth/logout`, { withCredentials: true })
        navigate("/login");
    };
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

                        <Link to={"/dashboard/sharedFiles"} onClick={() => { setOpenSidenav(false) }}>
                            <ListItem>
                                <ListItemPrefix>
                                    <InboxIcon className="h-6 w-6" />
                                </ListItemPrefix>
                                Shared With Me
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

                        <ListItem onClick={logOut}>
                            <div className="flex flex-row w-full">
                                <ListItemPrefix>
                                    <PowerIcon className="h-6 w-6" />
                                </ListItemPrefix>
                                Log Out
                            </div>
                        </ListItem>
                    </List>
                </Card>
            </Drawer>
        </>
    );
}