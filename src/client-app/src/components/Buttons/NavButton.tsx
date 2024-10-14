import React from 'react';
import { useNavigate } from 'react-router-dom';
import NavButtonType from "../../types/NavButtonType";

const NavButton: React.FC<NavButtonType> = ({ buttonText, redirectTo, width = 'w-full', height = 'h-[47px]' }) => {
    const navigate = useNavigate();

    const handleClick = () => {
        navigate(redirectTo);
    };

    return (
        <button
            type="button"
            className={`${width} ${height} flex items-center justify-center py-2 border-2 border-primary bg-primary text-center text-textPrimary text-[1rem] mt-4 rounded-full transition duration-500 ease-in-out hover:bg-hover hover:border-hover hover:scale-105 active:scale-90 active:bg-transparent active:border-primary active:text-textSecondary focus:scale-100 transform mx-auto`}
            onClick={handleClick}
        >
            {buttonText}
        </button>
    );
};

export default NavButton;