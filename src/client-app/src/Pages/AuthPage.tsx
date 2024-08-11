import React, {useState} from 'react';
import LoginForm from '../components/LoginForm';
import googleIcon from "../assets/icon-google.svg";
import RegisterForm from "../components/RegisterForm.tsx";
//import facebookIcon from '../assets/icon-facebook.svg';

const LoginPage: React.FC = () => {
    const [isLogin, setIsLogin] = useState(true);
    return (
        <div className="bg-gray-100 min-h-screen flex flex-col items-center justify-center">
            <div className="bg-white p-8 rounded-lg shadow-lg max-w-md w-full">
                <h1 className="text-center text-[1rem] font-inter font-bold mb-4">TrendLink</h1>
                <p className="text-center text-[1.45rem] font-inter font-light text-gray-600 mb-8">
                    A network for successful cooperation with bloggers
                </p>

                <div className="flex justify-center mb-8">
                    <button
                        onClick={() => setIsLogin(true)}
                        className={`pr-28 pl-10 py-2 border-b-2 text-[0.85rem] ${isLogin ? 'border-black text-black' : 'border-gray-300 text-gray-500'}`}
                    >
                        Login
                    </button>
                    <button
                        onClick={() => setIsLogin(false)}
                        className={`pl-28 pr-10 py-2 border-b-2 text-[0.85rem] ${!isLogin ? 'border-black text-black' : 'border-gray-300 text-gray-500'}`}
                    >
                        Register
                    </button>
                </div>

                {isLogin ? <LoginForm /> : <RegisterForm />}

                <div className="text-center text-gray-500 text-sm mt-4 text-[0.85rem]">
                    By clicking the "{isLogin ? 'Login' : 'Register'}" button, you accept the terms of the <a href="http://google.com"
                                                                                   className="text-black">Privacy
                    Policy</a>
                </div>
                <div className="text-center text-gray-500 text-sm mt-4">
                    or
                </div>
                <div className="flex justify-center mt-4 space-x-4">
                    <img src={googleIcon} alt="Google"
                         className="h-8 w-8 transition-transform duration-300 ease-in-out hover:scale-125 transform-origin-center"/>
                    {/*<img src="../assets/icon-facebook.svg" alt="Facebook" className="h-8 w-8"/>*/}
                </div>
            </div>


        </div>
    );
};

export default LoginPage;