import React from "react";
import imgTwo from "../assets/img2CallBackPage.svg";
import {useNavigate} from "react-router-dom";

const CallBackPage: React.FC = () => {

    const navigate = useNavigate();

    const handleclick = () => {
        navigate("/login");
    }

    return (
        <div className="bg-background flex justify-between items-center h-screen w-screen">
            <div className="pb-64 max-w-xl w-full">
                <div className="text-center">
                    <h1 className="text-center text-[1rem] font-kodchasan font-bold text-textSecondary mb-[1.5rem]">TrendLink</h1>
                    <p className="text-center text-[1.50rem] font-inter font-regular text-textSecondary mb-8">
                        Registration was successful!
                    </p>
                    <button
                        type="submit"
                        className="w-[310px] h-[47px] py-2 border-2 border-primary bg-primary text-textPrimary text-[1rem] mt-4 rounded-full transition duration-500 ease-in-out hover:bg-hover hover:border-hover hover:scale-105 active:scale-90 active:bg-transparent active:border-primary active:text-textSecondary focus:scale-100 transform"
                        onClick={handleclick}
                    >
                        Continue
                    </button>
                </div>

            </div>
            <div className="w-full h-full flex justify-center items-center">
                <img src={imgTwo} alt="" className="w-[60rem] h-[43rem] pr-4"/>
            </div>
        </div>
    );
}

export default CallBackPage;