import React from "react";
import { Fragment } from "react";
import { useSignOut } from "react-auth-kit";
import { useNavigate } from "react-router-dom";

export default function LogoutWrapper({ children }: { children: React.ReactNode }) {
    const signOut = useSignOut()
    const navigate = useNavigate()

    return (
        <div role="button" onClick={() => { signOut(); navigate("/") }}>
            {children}
        </div>

    )
}