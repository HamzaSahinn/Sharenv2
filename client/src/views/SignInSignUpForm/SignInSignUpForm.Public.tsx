import React, { useEffect, useState } from 'react';
import './index.css';
import blueLogo from "./../../assets/image/logoBlue.jpg"
import axios from 'axios';
import { useSignIn } from 'react-auth-kit'
import { useNavigate, useSearchParams } from 'react-router-dom';
type SignInData = {
    signInEmail: string,
    signInPassword: string
}

type SignUpData = {
    signUpEmail: string,
    signUpPassword: string,
    signUpName: string
}

type ErrorData = {
    signInErrorMessage: string,
    signUpErrorMessage: string
}

export default function SignInSignUp() {

    const [isSignIn, setIsSignIn] = useState<boolean>(true)
    const [signInForm, setSignInForm] = useState<SignInData>({ signInEmail: '', signInPassword: '' })
    const [signUpForm, setSignUpForm] = useState<SignUpData>({ signUpEmail: '', signUpPassword: '', signUpName: '' })
    const [error, setError] = useState()
    const signIn = useSignIn()
    const navigation = useNavigate()
    const [queryParameters] = useSearchParams()

    useEffect(() => {
        if (queryParameters.has('signIn')) {
            setIsSignIn(queryParameters.get('signIn') === 'true')
        }
    }, [])



    function handleSignInFormChange(e: any) {
        setSignInForm(prev => ({ ...prev, [e.target.name]: e.target.value }))
    }

    function handleSignUpFormChange(e: any) {
        setSignUpForm(prev => ({ ...prev, [e.target.name]: e.target.value }))
    }

    async function handleSignIn(e: any) {
        try {
            e.preventDefault()
            if (Object.values(signInForm).every(obj => { return obj })) {

                const res = await axios.post('http://localhost:8080/auth/login', { email: signInForm.signInEmail, password: signInForm.signInPassword })
                if (signIn(
                    {
                        token: res.data.token,
                        expiresIn: res.data.expiresIn,
                        tokenType: "Bearer",
                        authState: res.data.authUserState,

                    }
                )) {
                    navigation("/dashboard")

                } else {
                    //Throw error
                }
            }

        }
        catch (error) {
            console.log(error)
        }

    }

    async function handleSignUp() {

    }

    return (
        <div className='flex flex-col gap-12 items-center'>
            <a href='/' className='flex flex-row gap-4 items-center'>
                <img src={blueLogo} className='rounded-lg w-16 h-16' alt='Sharenv Logo' />
                <span className='text-white text-5xl font-semibold'>SHARENV</span>
            </a>
            <div id="main" className={`bg-gray-800 relative overflow-hidden w-[900px] h-[550px] ${!isSignIn && 's--signup'} border border-gray-900 shadow-lg rounded-lg `}>
                <div className="form sign-in">
                    <h2 className='text-white'>Welcome Back</h2>
                    <div className='w-96 mx-auto'>
                        <label htmlFor="signInEmail" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Email Address</label>
                        <input name='signInEmail' type="email" id="signInEmail" onChange={handleSignInFormChange}
                            className="border text-sm rounded-lg block w-full p-2.5 bg-gray-700 border-gray-600 placeholder-gray-400 text-white focus:ring-blue-500 focus:border-blue-500" placeholder="john.doe@company.com" required />
                    </div>
                    <div className='w-96 mx-auto'>
                        <label htmlFor="signInPassword" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Password</label>
                        <input name='signInPassword' type="password" id="signInPassword" onChange={handleSignInFormChange}
                            className="border text-sm rounded-lg block w-full p-2.5 bg-gray-700 border-gray-600 placeholder-gray-400 text-white focus:ring-blue-500 focus:border-blue-500" placeholder="*************" required />
                    </div>
                    <p className="forgot-pass">Forgot password?</p>
                    <button type="button" onClick={handleSignIn}
                        className="bg-blue-500 hover:bg-blue-600 mt-5 border rounded-lg">
                        Sign In
                    </button>
                </div>
                <div className="sub-cont absolute overflow-hidden bg-gray-800  w-[900px] h-full">
                    <div className="img">
                        <div className="img__text m--up">
                            <h2>New here?</h2>
                            <p>Sign up and discover great amount of new opportunities!</p>
                        </div>
                        <div className="img__text m--in">
                            <h2>One of us?</h2>
                            <p>If you already have an account, just sign in. We've missed you!</p>
                        </div>
                        <div
                            className="img__btn text-md cursor-pointer text-blue-500 border-2 border-blue-600 rounded-xl shadow-xl shadow-blue-500/50 font-semibold"
                            onClick={e => { e.preventDefault(); setIsSignIn(prev => !prev) }}>
                            <span className="m--up">Sign Up</span>
                            <span className="m--in">Sign In</span>
                        </div>
                    </div>

                    <div className="form sign-up">
                        <h2 className='text-white'>Time To Share and Live</h2>
                        <div className='w-96 mx-auto'>
                            <label htmlFor="signUpName" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Full Name</label>
                            <input name='signUpName' type="email" id="signUpName" onClick={handleSignUpFormChange}
                                className="border text-sm rounded-lg block w-full p-2.5 bg-gray-700 border-gray-600 placeholder-gray-400 text-white focus:ring-blue-500 focus:border-blue-500" placeholder="john.doe@company.com" required />
                        </div>
                        <div className='w-96 mx-auto'>
                            <label htmlFor="signUpEmail" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Email Address</label>
                            <input name='signUpEmail' type="email" id="signUpEmail" onClick={handleSignUpFormChange}
                                className="border text-sm rounded-lg block w-full p-2.5 bg-gray-700 border-gray-600 placeholder-gray-400 text-white focus:ring-blue-500 focus:border-blue-500" placeholder="john.doe@company.com" required />
                        </div>
                        <div className='w-96 mx-auto'>
                            <label htmlFor="signUpPassword" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Password</label>
                            <input name='signUpPassword' type="password" id="signUpPassword" onClick={handleSignUpFormChange}
                                className="border text-sm rounded-lg block w-full p-2.5 bg-gray-700 border-gray-600 placeholder-gray-400 text-white focus:ring-blue-500 focus:border-blue-500" placeholder="*************" required />
                        </div>
                        <button
                            type="button" className="bg-blue-500 hover:bg-blue-600 mt-5 border rounded-lg">
                            Sign Up
                        </button>
                    </div>
                </div>
            </div>
        </div>
    );
}