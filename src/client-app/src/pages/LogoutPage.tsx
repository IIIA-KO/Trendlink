import React, {useEffect} from "react";
import {useNavigate} from "react-router-dom";
import useAuth from "../hooks/useAuth";
import LoadingPage from "./LoadingPage";


const LogoutPage: React.FC = () => {

    const { logout } = useAuth();
    const navigate = useNavigate();

    useEffect(() => {
        logout();
        navigate("/login");
    }, []);

    return (
        <LoadingPage />
    );
}

export default LogoutPage;