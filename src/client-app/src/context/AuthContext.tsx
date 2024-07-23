import React, { createContext, useContext, useState, ReactNode } from 'react';
import { getAuthToken, login, logout } from '../services/AuthService';

interface AuthContextProps {
    isAuthenticated: boolean;
    login: (email: string, password: string) => Promise<void>;
    logout: () => void;
}

interface AuthProviderProps {
    children: ReactNode;
}

const AuthContext = createContext<AuthContextProps | undefined>(undefined);

export const AuthProvider: React.FC<AuthProviderProps> = ({ children }) => {
    const [isAuthenticated, setIsAuthenticated] = useState(!!getAuthToken());

    const handleLogin = async (email: string, password: string) => {
        await login(email, password);
        setIsAuthenticated(true);
    };

    const handleLogout = () => {
        logout();
        setIsAuthenticated(false);
    };

    return (
        <AuthContext.Provider value={{ isAuthenticated, login: handleLogin, logout: handleLogout }}>
            {children}
        </AuthContext.Provider>
    );
};

export const useAuth = () => {
    const context = useContext(AuthContext);
    if (!context) {
        throw new Error('useAuth must be used within an AuthProvider');
    }
    return context;
};