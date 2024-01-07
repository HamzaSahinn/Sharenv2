import React, { useEffect, useState } from "react";
import "./index.css";
import blueLogo from "./../../assets/image/logoBlue.jpg";
import axios from "axios";
import { useNavigate, useSearchParams } from "react-router-dom";
import { Alert, Button } from "@material-tailwind/react";
import { useCookies } from "react-cookie";

type SignInData = {
    signInEmail: string;
    signInPassword: string;
};

type SignUpData = {
    signUpEmail: string;
    signUpPassword: string;
    signUpName: string;
};

type ErrorData = {
    signInErrorMessage: string;
    signUpErrorMessage: string;
};

export default function SignInSignUp() {
    const [isSignIn, setIsSignIn] = useState<boolean>(true);
    const [signInForm, setSignInForm] = useState<SignInData>({
        signInEmail: "",
        signInPassword: "",
    });
    const [signUpForm, setSignUpForm] = useState<SignUpData>({
        signUpEmail: "",
        signUpPassword: "",
        signUpName: "",
    });
    const [error, setError] = useState<{
        isSignIn: boolean;
        value: string | null;
    }>();
    const [isLoading, setIsLoading] = useState<boolean>(false);

    const navigation = useNavigate();
    const [queryParameters] = useSearchParams();

    useEffect(() => {
        if (queryParameters.has("signIn")) {
            setIsSignIn(queryParameters.get("signIn") === "true");
        }
    }, []);

    const [cookies, setCookie] = useCookies(["jwt"]);
    const navigate = useNavigate();

    useEffect(() => {
        if (cookies.jwt) {
            navigate("/dashboard");
        }
    }, [cookies, navigate]);

    function handleSignInFormChange(e: any) {
        setSignInForm((prev) => ({ ...prev, [e.target.name]: e.target.value }));
    }

    function handleSignUpFormChange(e: any) {
        setSignUpForm((prev) => ({ ...prev, [e.target.name]: e.target.value }));
    }

    async function handleSignIn(e: any) {
        try {
            e.preventDefault();
            if (
                Object.values(signInForm).every((obj) => {
                    return obj;
                })
            ) {
                setIsLoading(true);

                const res = await axios.post(
                    `${process.env.REACT_APP_API_ROUTE}/auth/login`,
                    {
                        email: signInForm.signInEmail,
                        password: signInForm.signInPassword,
                    },
                    {
                        withCredentials: true,
                    }
                );

                if (res.status === 200) {
                    navigation("/dashboard");
                } else {
                    setError({ isSignIn: true, value: "Creditentials Wrong!" });
                }
            } else {
                setError({ isSignIn: true, value: "All fields must be filled out!" });
            }
        } catch (error) {
            console.log(error);
            setError({ isSignIn: true, value: "Creditentials Wrong!" });
        } finally {
            setIsLoading(false);
        }
    }

    async function handleSignUp(e: any) {
        try {
            e.preventDefault();
            if (
                Object.values(signUpForm).every((obj) => {
                    return obj;
                })
            ) {
                setIsLoading(true);

                const res = await axios.post(
                    `${process.env.REACT_APP_API_ROUTE}/auth/register`,
                    {
                        name: signUpForm.signUpName,
                        email: signUpForm.signUpEmail,
                        password: signUpForm.signUpPassword,
                    },
                    {
                        withCredentials: true,
                    }
                );
                if (res.status === 200) {
                    navigation("/dashboard");
                } else {
                    setError({ isSignIn: false, value: "Registration Failed!" });
                }
            } else {
                setError({ isSignIn: false, value: "All fields must be filled out!" });
            }
        } catch (error) {
            console.log(error);
            setError({ isSignIn: false, value: "Unknown Error" });
        } finally {
            setIsLoading(false);
        }
    }

    return (
        <div className="flex flex-col gap-12 items-center">
            <a href="/" className="flex flex-row gap-4 items-center mt-6">
                <img
                    src={blueLogo}
                    className="rounded-lg w-16 h-16"
                    alt="Sharenv Logo"
                />
                <span className="text-white text-5xl font-semibold">SHARENV</span>
            </a>
            <div
                id="main"
                className={`bg-gray-900 relative overflow-hidden w-[900px] h-[550px] ${!isSignIn && "s--signup"
                    } border border-gray-900 shadow-lg rounded-lg `}
            >
                <div className="form sign-in">
                    <h2 className="text-white">Welcome Back</h2>
                    <div className="w-96 mx-auto">
                        <label
                            htmlFor="signInEmail"
                            className="block mb-2 text-sm font-medium text-white"
                        >
                            Email Address
                        </label>
                        <input
                            name="signInEmail"
                            type="email"
                            id="signInEmail"
                            onChange={handleSignInFormChange}
                            className="border text-sm rounded-lg block w-full p-2.5 bg-gray-700 border-gray-600 placeholder-gray-400 text-white focus:ring-blue-500 focus:border-blue-500"
                            placeholder="john.doe@company.com"
                            required
                        />
                    </div>
                    <div className="w-96 mx-auto">
                        <label
                            htmlFor="signInPassword"
                            className="block mb-2 text-sm font-medium text-white"
                        >
                            Password
                        </label>
                        <input
                            name="signInPassword"
                            type="password"
                            id="signInPassword"
                            onChange={handleSignInFormChange}
                            className="border text-sm rounded-lg block w-full p-2.5 bg-gray-700 border-gray-600 placeholder-gray-400 text-white focus:ring-blue-500 focus:border-blue-500"
                            placeholder="*************"
                            required
                        />
                    </div>
                    <p className="forgot-pass">Forgot password?</p>
                    <Button
                        loading={isLoading}
                        variant="filled"
                        color="white"
                        type="button"
                        onClick={handleSignIn}
                        className="mt-4"
                    >
                        Sign In
                    </Button>
                    <Alert
                        className="mt-4"
                        color="red"
                        open={error?.isSignIn && error.value ? true : false}
                        onClose={() => {
                            setError({ isSignIn: true, value: null });
                        }}
                    >
                        {error?.value}
                    </Alert>
                </div>
                <div className="sub-cont absolute overflow-hidden bg-gray-900  w-[900px] h-full">
                    <div className="img">
                        <div className="img__text m--up">
                            <h2>New here?</h2>
                            <p>Sign up and discover great amount of new opportunities!</p>
                        </div>
                        <div className="img__text m--in">
                            <h2>One of us?</h2>
                            <p>
                                If you already have an account, just sign in. We've missed you!
                            </p>
                        </div>
                        <div
                            className="img__btn text-md cursor-pointer text-blue-500 border-2 border-blue-600 rounded-xl shadow-xl shadow-blue-500/50 font-semibold"
                            onClick={(e) => {
                                e.preventDefault();
                                setIsSignIn((prev) => !prev);
                            }}
                        >
                            <span className="m--up">Sign Up</span>
                            <span className="m--in">Sign In</span>
                        </div>
                    </div>

                    <div className="form sign-up">
                        <h2 className="text-white">Time To Share and Live</h2>
                        <div className="w-96 mx-auto">
                            <label
                                htmlFor="signUpName"
                                className="block mb-2 text-sm font-medium text-white"
                            >
                                Full Name
                            </label>
                            <input
                                name="signUpName"
                                type="text"
                                id="signUpName"
                                onChange={handleSignUpFormChange}
                                className="border text-sm rounded-lg block w-full p-2.5 bg-gray-700 border-gray-600 placeholder-gray-400 text-white focus:ring-blue-500 focus:border-blue-500"
                                placeholder="john.doe@company.com"
                                required
                            />
                        </div>
                        <div className="w-96 mx-auto">
                            <label
                                htmlFor="signUpEmail"
                                className="block mb-2 text-sm font-medium text-white"
                            >
                                Email Address
                            </label>
                            <input
                                name="signUpEmail"
                                type="email"
                                id="signUpEmail"
                                onChange={handleSignUpFormChange}
                                className="border text-sm rounded-lg block w-full p-2.5 bg-gray-700 border-gray-600 placeholder-gray-400 text-white focus:ring-blue-500 focus:border-blue-500"
                                placeholder="john.doe@company.com"
                                required
                            />
                        </div>
                        <div className="w-96 mx-auto">
                            <label
                                htmlFor="signUpPassword"
                                className="block mb-2 text-sm font-medium text-white"
                            >
                                Password
                            </label>
                            <input
                                name="signUpPassword"
                                type="password"
                                id="signUpPassword"
                                onChange={handleSignUpFormChange}
                                className="border text-sm rounded-lg block w-full p-2.5 bg-gray-700 border-gray-600 placeholder-gray-400 text-white focus:ring-blue-500 focus:border-blue-500"
                                placeholder="*************"
                                required
                            />
                        </div>
                        <Button
                            loading={isLoading}
                            type="button"
                            color="white"
                            onClick={handleSignUp}
                            className="mt-4"
                        >
                            Sign Up
                        </Button>
                        <Alert
                            className="mt-4"
                            color="red"
                            open={!error?.isSignIn && error?.value ? true : false}
                            onClose={() => {
                                setError({ isSignIn: false, value: null });
                            }}
                        >
                            {error?.value}
                        </Alert>
                    </div>
                </div>
            </div>
        </div>
    );
}
