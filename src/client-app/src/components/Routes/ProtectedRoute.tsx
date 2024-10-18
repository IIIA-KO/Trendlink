import React from 'react';
import { Navigate } from 'react-router-dom';
import useAuth from '../../hooks/useAuth';
import ProtectedRouteType from '../../types/ProtectedRouteType'
import LoadingPage from "../../pages/LoadingPage";
import { useProfile } from '../../hooks/useProfile';

const ProtectedRoute: React.FC<ProtectedRouteType> = ({ children }) => {
    const { user, loading } = useAuth();
    const {user: userProfile } = useProfile();

    if (loading) {
        return <LoadingPage />;
    }

    if (!user) {
        return <Navigate to="/login" />
    } else if (!userProfile) {
        return <Navigate to="/link-instagram" />
    }
    
    return <>{children}</>;
};

export default ProtectedRoute;