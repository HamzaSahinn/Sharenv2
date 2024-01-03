import { Outlet } from "react-router-dom";
import { DashboardNavbar } from "../components/DashboardNavbar/DasboardNavbar";


export default function DashboardLayout() {

    return (
        <>
            <DashboardNavbar />
            <div className="p-5">
                <div className=" p-4 rounded-xl bg-gray-900 w-full ">
                    <Outlet />
                </div>
            </div>

        </>
    )
}