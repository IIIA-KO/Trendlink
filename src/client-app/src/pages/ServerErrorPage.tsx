import React from "react";
import NavButton from "../components/Buttons/NavButton";

const ServerErrorPage: React.FC = () => {

    return (
        <div className="bg-background flex justify-center items-center h-screen w-screen">
            <div className="py-8 px-4 mx-auto max-w-screen-xl lg:py-16 lg:px-6">
                <div className="mx-auto max-w-screen-sm text-center flex flex-col justify-center items-center">
                    <h1 className="mb-4 text-8xl tracking-tight font-inter font-bold text-main-green lg:text-9xl">500</h1>
                    <p className="mb-4 text-3xl tracking-tight font-inter font-medium text-main-black md:text-4xl">
                        Internal Server Error.
                    </p>
                    <p className="mb-4 text-lg font-inter font-light text-main-black">
                        We are already working to solve the problem.
                    </p>
                    <NavButton buttonText={'Contact support'} redirectTo={'/'} width={'w-[300px]'} height={'h-[50px]'}/>
                </div>
            </div>
        </div>
    );
}

export default ServerErrorPage;