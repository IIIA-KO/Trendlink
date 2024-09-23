import React, { createContext, useState, useEffect, ReactNode } from 'react';
import {useNavigate} from 'react-router-dom';
import { refreshAccessToken } from '../services/auth';
import { AuthContextType} from '../types/AuthContextType';
import {AuthResponseType } from "../types/AuthResponseType";

const AuthContext = createContext<AuthContextType | undefined>(undefined);

const AuthProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    const [user, setUser] = useState<AuthResponseType | null>(null);
    const navigate = useNavigate();

    useEffect(() => {
        const accessToken = localStorage.getItem('accessToken');
        const refreshToken = localStorage.getItem('refreshToken');
        const expiresIn = localStorage.getItem('expiresIn');
        const storedTime = localStorage.getItem('storedTime');

        if (accessToken && refreshToken && expiresIn && storedTime) {
            const now = Date.now();
            const elapsed = (now - Number(storedTime)) / 1000;
            const remainingTime = Number(expiresIn) - elapsed;

            if (remainingTime > 0) {
                setUser({ accessToken, refreshToken, expiresIn: remainingTime });
                scheduleTokenRefresh(remainingTime);
            } else {
                logout();
            }
        } else {
            logout();
        }
    }, []);

    const scheduleTokenRefresh = (expiresIn: number) => {
        setTimeout(async () => {
            await refreshTokens();
        }, (expiresIn - 60) * 1000);
    };

    const login = (userData: AuthResponseType) => {
        const { accessToken, refreshToken, expiresIn } = userData;
        localStorage.setItem('accessToken', accessToken);
        localStorage.setItem('refreshToken', refreshToken);
        localStorage.setItem('expiresIn', String(expiresIn));
        localStorage.setItem('storedTime', String(Date.now()));
        setUser(userData);
        navigate('/');
        scheduleTokenRefresh(expiresIn);
    };

    const logout = () => {
        localStorage.removeItem('accessToken');
        localStorage.removeItem('refreshToken');
        localStorage.removeItem('expiresIn');
        localStorage.removeItem('storedTime');
        setUser(null);
        navigate('/login');
    };

    const refreshTokens = async () => {
        const refreshToken = localStorage.getItem('refreshToken');
        if (refreshToken) {
            try {
                const { accessToken, expiresIn } = await refreshAccessToken(refreshToken);
                localStorage.setItem('accessToken', accessToken);
                localStorage.setItem('expiresIn', String(expiresIn));
                localStorage.setItem('storedTime', String(Date.now()));
                setUser((prevUser) => prevUser ? { ...prevUser, accessToken } : null);
                scheduleTokenRefresh(expiresIn);
            } catch (error) {
                console.error('Failed to refresh token', error);
                logout();
            }
        }
    };

    return (
        <AuthContext.Provider value={{ user, login, logout, refreshTokens }}>
            {children}
        </AuthContext.Provider>
    );
};


export { AuthProvider, AuthContext };