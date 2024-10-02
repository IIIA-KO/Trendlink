import axiosInstance from './api';
import { UserType } from '../types/UserType';
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
        const response = await axiosInstance.post('/users/register', data);
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

export const login = async (credentials: UserType): Promise<AuthResponseType | null> => {
    try {
        const response = await axiosInstance.post('/users/login', credentials);
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
        await axiosInstance.post('/users/logout');
    } catch (error) {
        handleError(error);
    }
};

export const refreshAccessToken = async (refreshToken: string): Promise<AuthResponseType | null> => {
    try {
        const response = await axiosInstance.post('/users/refresh', { refreshToken });
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