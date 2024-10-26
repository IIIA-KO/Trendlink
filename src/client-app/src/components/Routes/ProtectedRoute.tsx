import React from 'react';
import { Navigate } from 'react-router-dom';
import useAuth from '../../hooks/useAuth';
import ProtectedRouteType from '../../types/ProtectedRouteType'
import LoadingPage from "../../pages/LoadingPage";
import { useUser } from "../../hooks/useUser";


const ProtectedRoute: React.FC<ProtectedRouteType> = ({ children }) => {
    const { user, loading } = useAuth();
    const { user: userProfile } = useUser()

    if (loading) {
        return <LoadingPage />;
    }

    if (!user) {
        return <Navigate to="/login" />;
    }

    if (!userProfile) {
        return <LoadingPage />;
    }
    
    return <>{children}</>;
};

export default ProtectedRoute;