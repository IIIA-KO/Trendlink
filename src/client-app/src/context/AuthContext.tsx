import React, { createContext, useState, useEffect, ReactNode } from 'react';
import { Navigate } from 'react-router-dom';
import { refreshAccessToken } from '../api/authApi';
import { AuthContextType} from '../models/AuthContextType.ts';
import {AuthResponseType } from "../models/AuthResponseType.ts";

const AuthContext = createContext<AuthContextType | undefined>(undefined);

const AuthProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    const [user, setUser] = useState<AuthResponseType | null>(null);
    const [redirect, setRedirect] = useState<string | null>(null);

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
        setRedirect('/');
        scheduleTokenRefresh(expiresIn);
    };

    const logout = () => {
        localStorage.removeItem('accessToken');
        localStorage.removeItem('refreshToken');
        localStorage.removeItem('expiresIn');
        localStorage.removeItem('storedTime');
        setUser(null);
        setRedirect('/login');
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
            {redirect && <Navigate to={redirect} />}
            {children}
        </AuthContext.Provider>
    );
};


export { AuthProvider, AuthContext };