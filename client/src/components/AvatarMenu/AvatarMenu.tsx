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
import { Link } from "react-router-dom";
import defaultProfilePhoto from "../../assets/image/userMenuPicSkeleton.png"
import { useContext } from "react";
import { AuthContext } from "../../context/authContext";
export function AvatarMenu() {

    const { _id, name, mail } = useContext(AuthContext);

    return (
        <Menu>
            <MenuHandler>
                <Avatar
                    variant="circular"
                    alt="profile photo"
                    className="cursor-pointer"
                    src={defaultProfilePhoto}
                />
            </MenuHandler>
            <MenuList>
                <MenuItem>
                    <Typography variant="small" color="blue-gray" className="font-medium flex flex-row gap-2 items-center">
                        {name}
                    </Typography>
                </MenuItem>
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
                <MenuItem className="flex items-center gap-2 ">
                    <Typography variant="small" color="blue-gray" className="font-medium flex flex-row gap-2 items-center">
                        <PowerIcon className="w-5 h-5" />
                        Sign Out
                    </Typography>
                </MenuItem>

            </MenuList>
        </Menu>
    );
}