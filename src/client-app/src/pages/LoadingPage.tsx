import React from "react";
import logo from '../assets/logo-trendlink.svg'

const LoadingPage: React.FC = () => {
    return (
        <div className="bg-background flex justify-center items-center h-screen w-screen">
            <div className="relative flex flex-col items-center justify-center">
                <div className="absolute top-[-240px] flex items-center justify-center w-[320px] h-[226px]">
                    <img
                        src={logo}
                        alt="logo"
                        className="h-[121px] w-[160px] object-contain"
                    />
                </div>
                <div
                    className="spinner relative h-[113px] w-[113px] rounded-full bg-gradient-to-r from-teal-500 via-transparent to-transparent"/>
                <div className="absolute inset-0 flex items-center justify-center">
                    <p className="text-main-black font-kodchasan font-bold text-[12px]">Loading</p>
                </div>
            </div>
        </div>
    );
}

export default LoadingPage;