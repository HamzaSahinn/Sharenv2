import { Outlet, useNavigate } from "react-router-dom";
import { DashboardNavbar } from "../components/DashboardNavbar/DasboardNavbar";
import { useEffect, useState } from "react";
import { useCookies } from "react-cookie";
import axios from "axios";
import { AuthContext } from "../context/authContext";

export default function DashboardLayout() {
    const navigate = useNavigate();
    const [cookies, setCookie, removeCookie] = useCookies(["jwt"]);
    const [user, setUser] = useState({ _id: "", mail: "", name: "" })

    useEffect(() => {
        const verifyUser = async () => {
            if (!cookies.jwt) {
                navigate("/login");
            } else {
                const { data } = await axios.post(
                    `${process.env.REACT_APP_API_ROUTE}/auth`,
                    {},
                    {
                        withCredentials: true,
                    }
                );
                if (!data.status) {
                    removeCookie("jwt");
                    navigate("/login");
                } else {
                    setUser({ _id: data.userId, mail: data.userEmail, name: data.userName })
                }
            }
        };
        verifyUser();
    }, [cookies, navigate, removeCookie]);


    return (
        <>
            <AuthContext.Provider value={{ _id: user._id, mail: user.mail, name: user.name }}>
                <DashboardNavbar />
                <div className="p-5">
                    <div className=" p-4 rounded-xl bg-gray-900 w-full ">
                        <Outlet />
                    </div>
                </div>
            </AuthContext.Provider>
        </>
    )
}