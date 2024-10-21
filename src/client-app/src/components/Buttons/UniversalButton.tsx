import React from 'react';
import { useNavigate } from 'react-router-dom';
import {UniversalButtonType} from "../../types/UniversalButtonType";

const NavButton: React.FC<UniversalButtonType> = ({
                                                buttonText,
                                                redirectTo,
                                                onClick,
                                                width = 'w-full',
                                                height = 'h-[47px]',
                                                className = ''
                                            }) => {
    const navigate = useNavigate();

    const handleClick = () => {
        if (onClick) {
            onClick();
        } else if (redirectTo) {
            navigate(redirectTo);
        }
    };

    return (
        <button
            type="button"
            className={`${width} ${height} ${className} flex items-center justify-center py-2 border-2 border-primary bg-primary text-center text-textPrimary text-[1rem] mt-4 rounded-full transition duration-500 ease-in-out hover:bg-hover hover:border-hover hover:scale-105 active:scale-90 active:bg-transparent active:border-primary active:text-textSecondary focus:scale-100 transform`}
            onClick={handleClick}
        >
            {buttonText}
        </button>
    );
};

export default NavButton;