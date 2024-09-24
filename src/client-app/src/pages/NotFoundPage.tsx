import React from "react";
import {useNavigate} from "react-router-dom";
import NavButton from "../components/Buttons/NavButton";

const NotFoundPage: React.FC = () => {
    return (
        <div className="bg-background flex justify-center items-center h-screen w-screen">
            <div className="py-8 px-4 mx-auto max-w-screen-xl lg:py-16 lg:px-6">
                <div className="mx-auto max-w-screen-sm text-center">
                    <h1 className="mb-4 text-8xl tracking-tight font-inter font-bold lg:text-9xl text-transparent" style={{WebkitTextStroke: "2px #009EA0",}}>
                        404
                    </h1>
                    <p className="mb-4 text-3xl tracking-tight font-inter font-medium text-main-black md:text-4xl">
                        Something's missing.
                    </p>
                    <p className="mb-4 text-lg font-inter font-light text-main-black">
                        Sorry, we can't find that page. You'll find lots to explore on the home page.
                    </p>
                    <NavButton buttonText={'Back to Homepage'} redirectTo={'/'} width={'w-[300px]'} height={'h-[50px]'} />
                </div>
            </div>
        </div>
    );
}

export default NotFoundPage;