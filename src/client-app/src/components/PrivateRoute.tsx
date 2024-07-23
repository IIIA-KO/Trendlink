import React from 'react';
import { Route, Navigate } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';

interface PrivateRouteProps {
    component: React.ComponentType;
    path: string;
    [key: string]: any;
}

const PrivateRoute: React.FC<PrivateRouteProps> = ({ component: Component, ...rest }) => {
    const { isAuthenticated } = useAuth();
    return (
        <Route
            {...rest}
            element={isAuthenticated ? <Component /> : <Navigate to="/login" />}
        />
    );
};

export default PrivateRoute;