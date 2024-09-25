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
}): Promise<AuthResponseType> => {
    try {
        const response = await axiosInstance.post('/users/register', data);
        return {
            accessToken: response.data.accessToken,
            refreshToken: response.data.refreshToken,
            expiresIn: response.data.expiresIn,
        };
    } catch (error: unknown) {
        handleError(error);
        return {
            accessToken: '',
            refreshToken: '',
            expiresIn: 0,
        };
    }
};

export const login = async (credentials: UserType): Promise<AuthResponseType> => {
    try {
        const response = await axiosInstance.post('/users/login', credentials);
        return {
            accessToken: response.data.accessToken,
            refreshToken: response.data.refreshToken,
            expiresIn: response.data.expiresIn,
        };
    } catch (error: unknown) {
        handleError(error);
        return {
            accessToken: '',
            refreshToken: '',
            expiresIn: 0,
        };
    }
};

export const logout = async (): Promise<void> => {
    try {
        await axiosInstance.post('/users/logout');
    } catch (error) {
        handleError(error);
    }
};

export const refreshAccessToken = async (refreshToken: string): Promise<AuthResponseType> => {
    try {
        const response = await axiosInstance.post('/users/refresh', { refreshToken });
        return {
            accessToken: response.data.accessToken,
            refreshToken: response.data.refreshToken,
            expiresIn: response.data.expiresIn,
        };
    } catch (error: unknown) {
        handleError(error);
        return {
            accessToken: '',
            refreshToken: '',
            expiresIn: 0,
        };
    }
};