import React from "react";

const LoadingPage: React.FC = () => {
    return (
        <div className="bg-background flex justify-center items-center h-screen w-screen">
            <div className="py-8 px-4 mx-auto max-w-screen-xl lg:py-16 lg:px-6">
                <div className="mx-auto max-w-screen-sm text-center">
                    <h1 className="mb-4 text-8xl tracking-tight font-inter font-bold lg:text-9xl text-transparent" style={{WebkitTextStroke: "2px #009EA0",}}>
                        Loading....
                    </h1>
                </div>
            </div>
        </div>
    );
}

export default LoadingPage;