import React from "react";

const LoadingPage: React.FC = () => {
    return (
        <div className="bg-background flex justify-center items-center h-screen w-screen">
            <div className="relative">
                <div className="spinner relative h-[113px] w-[113px] rounded-full bg-gradient-to-r from-teal-500 via-transparent to-transparent" />
                <div className="absolute inset-0 flex items-center justify-center">
                    <p className="text-main-black font-kodchasan font-bold text-[12px]">Loading</p>
                </div>
            </div>
        </div>
    );
}

export default LoadingPage;