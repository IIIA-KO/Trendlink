import React, {useState} from 'react';
import LoginForm from '../components/Form/LoginForm.tsx';
import googleIcon from "../assets/icon-google.svg";
import RegisterForm from "../components/Form/RegisterForm.tsx";

const LoginPage: React.FC = () => {
    const [isLogin, setIsLogin] = useState(true);

    return (
        <div className="bg-white min-h-screen flex flex-col items-start justify-center">
            <div className="p-8 max-w-xl w-full">
                <div className="ml-28">
                    <h1 className="text-center text-[1rem] font-inter font-bold mb-4">TrendLink</h1>
                    <p className="text-center text-[1.45rem] font-inter font-light text-gray-600 mb-8">
                        A network for successful cooperation with bloggers
                    </p>

                    <div className="relative flex justify-center mb-8">
                        <button
                            onClick={() => setIsLogin(true)}
                            className={`relative z-10 pr-[7.4rem] pl-10 py-2 text-[0.85rem] ${isLogin ? 'text-black' : 'text-gray-500'}`}
                        >
                            Login
                        </button>
                        <button
                            onClick={() => setIsLogin(false)}
                            className={`relative z-10 pl-[7.4rem] pr-10 py-2 text-[0.85rem] ${!isLogin ? 'text-black' : 'text-gray-500'}`}
                        >
                            Register
                        </button>

                        <span className="absolute bottom-0 left-0 w-full h-[1px] bg-gray-300"></span>
                        <span
                            className={`absolute bottom-0 left-0 h-[1px] bg-black transition-all duration-300 ease-in-out ${isLogin ? 'w-[50%]' : 'w-[50%] translate-x-full'}`}
                        ></span>
                    </div>

                    {isLogin ? <LoginForm/> : <RegisterForm/>}

                    <div className="text-center text-gray-500 text-sm mt-2 text-[0.85rem]">
                        By clicking the "{isLogin ? 'Login' : 'Register'}" button, you accept the terms of the
                        <a
                        href="http://google.com"
                        className="text-black transition-transform duration-400 ease-in-out hover:text-blue-700"
                        > Privacy
                        Policy
                        </a>
                    </div>
                    <div className="text-center text-gray-500 text-sm mt-8">
                        or
                    </div>
                    <div className="flex justify-center mt-4 space-x-4">
                        <img src={googleIcon} alt="Google"
                             className="h-8 w-8 transition duration-500 ease-in-out hover:scale-125 transform-origin-center"/>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default LoginPage;