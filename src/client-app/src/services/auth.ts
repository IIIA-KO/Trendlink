import axiosInstance from './api';
import { UserLoginType } from '../types/UserLoginType';
import { AuthResponseType } from '../types/AuthResponseType'
import {handleError} from "../utils/handleError";

export const register = async (data: {
    firstName: string;
    lastName: string;
    birthDate: string;
    email: string;
    phoneNumber: string;
    password: string;
    stateId: string;
}): Promise<AuthResponseType | null> => {
    try {
        const response = await axiosInstance.post('/accounts/register', data);
        return {
            accessToken: response.data.accessToken,
            refreshToken: response.data.refreshToken,
            expiresIn: response.data.expiresIn,
        };
    } catch (error) {
        handleError(error);
        return null
    }
};

export const login = async (credentials: UserLoginType): Promise<AuthResponseType | null> => {
    try {
        const response = await axiosInstance.post('/accounts/login', credentials);
        return {
            accessToken: response.data.accessToken,
            refreshToken: response.data.refreshToken,
            expiresIn: response.data.expiresIn,
        };
    } catch (error) {
        handleError(error);
        return null
    }
};

export const logout = async (): Promise<void> => {
    try {
        await axiosInstance.post('/accounts/logout');
    } catch (error) {
        handleError(error);
    }
};

export const refreshAccessToken = async (refreshToken: string): Promise<AuthResponseType | null> => {
    try {
        const response = await axiosInstance.post('/accounts/refresh', { refreshToken });
        return {
            accessToken: response.data.accessToken,
            refreshToken: response.data.refreshToken,
            expiresIn: response.data.expiresIn,
        };
    } catch (error) {
        handleError(error);
        return null
    }
};