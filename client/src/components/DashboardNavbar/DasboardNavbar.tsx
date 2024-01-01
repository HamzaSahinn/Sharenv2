import React, { useState } from "react";
import {
    Navbar,
    MobileNav,
    Typography,
    Button,
    IconButton,
    Card,
} from "@material-tailwind/react";
import { Bars3Icon, XMarkIcon } from "@heroicons/react/24/solid";
import { Sidenav } from "../Sidenav/Sidenav";
import Logo from './../../assets/image/logoBlue.jpg'
import { useSignOut } from "react-auth-kit";

export function DashboardNavbar() {
    const [openNav, setOpenNav] = useState<boolean>(false);
    const [openSidenav, setOpenSidenav] = useState<boolean>(false)
    const signOut = useSignOut()

    React.useEffect(() => {
        window.addEventListener(
            "resize",
            () => window.innerWidth >= 960 && setOpenNav(false),
        );
    }, []);

    const navList = (
        <ul className="mt-2 mb-4 flex flex-col gap-2 lg:mb-0 lg:mt-0 lg:flex-row lg:items-center lg:gap-6">
            <Typography
                as="li"
                color="blue-gray"
                className="p-1 font-normal text-md"
            >
                <a href="#" className="flex items-center">
                    Pages
                </a>
            </Typography>
            <Typography
                as="li"
                color="white"
                className="p-1 font-normal text-md"
            >
                <a href="#" className="flex items-center">
                    Account
                </a>
            </Typography>
            <Typography
                as="li"
                color="blue-gray"
                className="p-1 font-normal text-md"
            >
                <a href="#" className="flex items-center">
                    Blocks
                </a>
            </Typography>
            <Typography
                as="li"
                color="blue-gray"
                className="p-1 font-normal text-md"
            >
                <a href="#" className="flex items-center">
                    Docs
                </a>
            </Typography>
        </ul>
    );

    return (
        <>
            <Sidenav openSidenav={openSidenav} setOpenSidenav={setOpenSidenav} />

            <div className="m-2 max-h-[768px] sticky top-0 z-10 ">
                <Navbar color="blue-gray" className="rounded-xl h-max max-w-full px-4 py-2 lg:px-8 lg:py-4">
                    <div className="flex flex-row items-center justify-between text-blue-gray-900">
                        <div className="flex flex-row items-center gap-3">
                            <IconButton variant="text" size="lg" onClick={() => setOpenSidenav(true)}>
                                {openSidenav ? (
                                    <XMarkIcon className="h-8 w-8 stroke-2" />
                                ) : (
                                    <Bars3Icon className="h-8 w-8 stroke-2" />
                                )}
                            </IconButton>
                            <Typography
                                as="a"
                                href="/dashboard"
                                className="mr-4 cursor-pointer py-1.5 text-xl flex flex-row gap-2 items-center"
                            >
                                <img className="w-auto h-10 rounded-lg" src={Logo} alt="Sharenv Logo" />
                                Sharenv
                            </Typography>
                        </div>

                        <div className="flex items-center gap-4">
                            <div className="mr-4 hidden lg:block">{navList}</div>
                        </div>
                    </div>
                    <MobileNav open={openNav}>
                        {navList}
                        <div className="flex items-center gap-x-1">
                            <Button fullWidth variant="text" size="sm" className="">
                                <span>Log In</span>
                            </Button>
                            <Button fullWidth variant="gradient" size="sm" className="">
                                <span>Sign in</span>
                            </Button>
                        </div>
                    </MobileNav>
                </Navbar>
            </div>
        </>

    );
}