import { useContext } from 'react';
import { AuthContext } from '../context/AuthContext';
import {useNavigate} from "react-router-dom";

const useAuth = () => {
    const context = useContext(AuthContext);
    const navigate = useNavigate();

    if (!context) {
        throw new Error('useAuth must be used within an AuthProvider');
    }

    return { ...context, navigate };
};

export default useAuth;