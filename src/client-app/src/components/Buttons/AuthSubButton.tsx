
const AuthSubButton: React.FC<{ buttonText: string }> = ({ buttonText }) => {
    return (
        <button
            type="submit"
            className="w-full h-[47px] mt-[0.3rem] py-2 border-2 border-primary bg-primary text-textPrimary text-[1rem] mt-4 rounded-full transition duration-500 ease-in-out hover:bg-hover hover:border-hover hover:scale-105 active:scale-90 active:bg-transparent active:border-primary active:text-textSecondary focus:scale-100 transform"
        >
            {buttonText}
        </button>
    );
};

export default AuthSubButton;