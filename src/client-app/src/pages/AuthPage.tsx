import React, {useState} from 'react';
import LoginForm from '../components/Forms/LoginForm';
import googleIcon from "../assets/icons/icon-google.svg";
import imgOne from "../assets/img1AuthPage.svg";
import RegisterForm from "../components/Forms/RegisterForm";
import GoogleRegisterForm from "../components/Forms/GoogleRegisterForm";
import GoogleLoginForm from "../components/Forms/GoogleLoginForm";
import logo from "../assets/logo/logo-trendlink-green.svg";

const AuthPage: React.FC = () => {
    const [isLogin, setIsLogin] = useState(true);
    const [showGoogleAuth, setShowGoogleAuth] = useState(false);

    const handleGoogleAuth = () => {
        setShowGoogleAuth(true);
    };

    return (
        <div className="bg-background flex justify-between items-center h-screen w-screen">
            <div className="p-8 pt-16 max-w-xl w-full">
                <div className="ml-16">
                    <div className="flex items-center justify-center mb-1">
                        <img
                            src={logo}
                            alt="logo"
                            className="h-[101px] w-[110px] object-contain"
                        />
                    </div>
                    <p className="text-center text-[1.50rem] font-inter font-regular text-textSecondary mb-2">
                        A network for successful cooperation with bloggers
                    </p>

                    <div className="relative flex justify-center mb-8">
                    <button
                            onClick={() => {
                                setIsLogin(true);
                                setShowGoogleAuth(false);
                            }
                        }
                            className={`relative z-10 pr-[7.4rem] pl-10 py-2 text-[0.85rem] ${isLogin ? 'text-black' : 'text-gray-500'}`}
                        >
                            Login
                        </button>
                        <button
                            onClick={() => {
                                setIsLogin(false)
                                setShowGoogleAuth(false);
                            }}
                            className={`relative z-10 pl-[7.4rem] pr-10 py-2 text-[0.85rem] ${!isLogin ? 'text-black' : 'text-gray-500'}`}
                        >
                            Register
                        </button>

                        <span className="absolute bottom-0 left-0 w-full h-[1px] bg-gray-300"></span>
                        <span
                            className={`absolute bottom-0 left-0 h-[1px] bg-black transition-all duration-300 ease-in-out ${isLogin ? 'w-[50%]' : 'w-[50%] translate-x-full'}`}
                        ></span>
                    </div>

                    {!showGoogleAuth && (isLogin ? <LoginForm/> : <RegisterForm/>)}
                    {showGoogleAuth && ( isLogin ? <GoogleLoginForm /> : <GoogleRegisterForm />)}

                    <div className="text-center text-gray-500 text-sm mt-2">
                        By clicking the "{isLogin ? 'Login' : 'Register'}" button, you accept the terms of the
                        <br/>
                        <a
                            href="http://google.com"
                            className="text-black transition-transform duration-400 ease-in-out hover:text-blue-700"
                        > Privacy
                            Policy
                        </a>
                    </div>
                    <div className="text-center text-gray-500 text-sm mt-1">
                        or
                    </div>
                    <div className="flex justify-center mt-1 space-x-4">
                        <img
                            src={googleIcon}
                            alt="Google"
                            className="h-8 w-8 transition duration-500 ease-in-out hover:scale-125 transform-origin-center"
                            onClick={handleGoogleAuth}
                        />
                    </div>

                </div>
            </div>
            <div className="w-full h-full flex justify-center items-center">
                <img src={imgOne} alt="" className="w-[60rem] h-[51rem]" />
            </div>
        </div>
    );
};

export default AuthPage;