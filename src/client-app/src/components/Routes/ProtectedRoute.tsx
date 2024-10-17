import React from 'react';
import { Navigate } from 'react-router-dom';
import useAuth from '../../hooks/useAuth';
import ProtectedRouteType from '../../types/ProtectedRouteType'
import LoadingPage from "../../pages/LoadingPage";

const ProtectedRoute: React.FC<ProtectedRouteType> = ({ children }) => {
    const { user, loading } = useAuth();

    if (loading) {
        return <LoadingPage />;
    }

    if (!user) {
        return <Navigate to="/login" />
    }

    if (!user.isInstagramLinked) {
        return <Navigate to="/link-instagram" />
    }

    return <>{children}</>;
};

export default ProtectedRoute;