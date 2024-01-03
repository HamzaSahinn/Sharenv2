import {
    Menu,
    MenuHandler,
    MenuList,
    MenuItem,
    Avatar,
    Typography,
} from "@material-tailwind/react";
import {
    ChartPieIcon,
    InboxIcon,
    PowerIcon,
    Cog6ToothIcon
} from "@heroicons/react/24/outline";
import LogoutWrapper from "../LogoutWrapper/LogoutWrapper";
import { Link } from "react-router-dom";
export function AvatarMenu() {
    return (
        <Menu>
            <MenuHandler>
                <Avatar
                    variant="circular"
                    alt="tania andrew"
                    className="cursor-pointer"
                    src="https://images.unsplash.com/photo-1633332755192-727a05c4013d?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1480&q=80"
                />
            </MenuHandler>
            <MenuList>
                <Link to={"/dashboard"}>
                    <MenuItem className="flex items-center gap-2">
                        <Typography variant="small" color="blue-gray" className="font-medium flex flex-row gap-2 items-center">
                            <ChartPieIcon className="w-6 h-6" />
                            Dashboard
                        </Typography>
                    </MenuItem>
                </Link>

                <Link to={"/dashboard/files"}>
                    <MenuItem className="flex items-center gap-2">
                        <Typography variant="small" color="blue-gray" className="font-medium flex flex-row gap-2 items-center">
                            <InboxIcon className="w-6 h-6" />
                            Files
                        </Typography>
                    </MenuItem>
                </Link>

                <MenuItem className="flex items-center gap-2">
                    <Typography variant="small" color="blue-gray" className="font-medium flex flex-row gap-2 items-center">
                        <Cog6ToothIcon className="w-6 h-6" />
                        Settings
                    </Typography>
                </MenuItem>

                <hr className="my-2 border-blue-gray-50" />
                <LogoutWrapper>
                    <MenuItem className="flex items-center gap-2 ">
                        <Typography variant="small" color="blue-gray" className="font-medium flex flex-row gap-2 items-center">
                            <PowerIcon className="w-5 h-5" />
                            Sign Out
                        </Typography>
                    </MenuItem>
                </LogoutWrapper>

            </MenuList>
        </Menu>
    );
}