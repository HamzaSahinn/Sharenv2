import "./index.css"
import Logo from './../../assets/image/logoBlue.jpg'
import { useState } from "react"
import axios from "axios"
import { useSignIn } from 'react-auth-kit'
import { useNavigate, useSearchParams } from 'react-router-dom';
import { Button } from "@material-tailwind/react"
type SignInData = {
    signInEmail: string,
    signInPassword: string
}

export default function Home() {
    const [signInForm, setSignInForm] = useState<SignInData>({ signInEmail: '', signInPassword: '' })
    const signIn = useSignIn()
    const navigation = useNavigate()
    const [isSignInLoading, setIsSignInLoading] = useState<boolean>(false)

    function handleSignInFormChange(e: any) {
        setSignInForm(prev => ({ ...prev, [e.target.name]: e.target.value }))
    }

    async function handleSignIn(e: any) {
        try {
            e.preventDefault()
            if (Object.values(signInForm).every(obj => { return obj })) {
                setIsSignInLoading(true)

                const res = await axios.post(`${process.env.REACT_APP_API_ROUTE}/auth/login`, { email: signInForm.signInEmail, password: signInForm.signInPassword })
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

                setIsSignInLoading(false)
            }

        }
        catch (error) {
            console.log(error)
            setIsSignInLoading(false)
        }

    }

    return (

        <header className="">
            <div className="container px-6 mx-auto">
                <nav className="flex flex-col py-6 sm:flex-row sm:justify-between sm:items-center">
                    <a href="/" className="text-4xl font-semibold flex flex-row gap-3 items-center text-white pr-4 hover:bg-blue-700/60 bg-blue-800/60 rounded-lg">
                        <img className="w-auto h-16 rounded-lg" src={Logo} alt="Sharenv Logo" />
                        SHARENV
                    </a>

                    <div className="flex items-center mt-2 -mx-2 sm:mt-0">
                        <a href="/login?signIn=true" className="px-3 py-1 font-semibold text-white transition-colors duration-300 transform border-2 rounded-md hover:bg-gray-700">Sign In</a>
                        <a href="/login?signIn=false" className="px-3 py-2 mx-2 font-semibold text-white transition-colors duration-300 transform bg-black rounded-md hover:bg-gray-800">Sign Up</a>
                    </div>
                </nav>

                <div className="flex flex-col items-center py-6 lg:h-[36rem] lg:flex-row px-12">
                    <div className="lg:w-1/2">
                        <h2 className="text-3xl font-semibold text-gray-100 lg:text-4xl">Sharenv</h2>

                        <h3 className="mt-2 text-2xl font-semibold text-gray-100">
                            Hello <span className="text-blue-400">Guest</span>
                        </h3>

                        <p className="mt-4 text-gray-100">Welcome to the sharing portal Sharenv. If you have a membership, you can log in immediately and start sharing files. If you don't have a membership yet, don't hesitate, sign up now and start sharing your files.</p>
                    </div>

                    <div className="flex mt-8 lg:w-1/2 lg:justify-end lg:mt-0">
                        <div className="w-full max-w-md bg-white rounded-lg dark:bg-gray-800">
                            <div className="px-6 py-8 text-center">
                                <h2 className="text-2xl font-semibold text-gray-700 dark:text-white fo">Sign In</h2>

                                <form>
                                    <div className="mt-4">
                                        <input className="block w-full px-4 py-2 text-gray-700 placeholder-gray-400 bg-white border rounded-md focus:border-blue-400 focus:ring-opacity-40 focus:ring-blue-300 focus:outline-none focus:ring"
                                            onChange={handleSignInFormChange} name="signInEmail" type="email" placeholder="Email address" aria-label="Email address" />
                                        <input className="block w-full px-4 py-2 mt-4 text-gray-700 placeholder-gray-400 bg-white border rounded-md dark:bg-gray-800 dark:border-gray-600 dark:placeholder-gray-500 focus:border-blue-400 dark:focus:border-blue-300 focus:ring-opacity-40 focus:ring-blue-300 focus:outline-none focus:ring"
                                            onChange={handleSignInFormChange} name="signInPassword" type="password" placeholder="Password" aria-label="Password" />
                                    </div>

                                    <div className="flex items-center justify-between mt-4">
                                        <a href="#" className="text-sm text-gray-600 dark:text-gray-200 hover:underline">Forget Password?</a>

                                        <Button loading={isSignInLoading} onClick={handleSignIn}
                                            className="px-6 py-2 ">
                                            Sign In
                                        </Button>
                                    </div>
                                </form>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </header>
    )
}