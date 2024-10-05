import React from 'react';
import { Navigate } from 'react-router-dom';
import useAuth from '../hooks/useAuth';
import ProtectedRouteType from '../types/ProtectedRouteType'

const ProtectedRoute: React.FC<ProtectedRouteType> = ({ children }) => {
    const { user } = useAuth();

    if (user === null) {
        return null;
    }

    return user ? <>{children}</> : <Navigate to="/login" />;
};

export default ProtectedRoute;