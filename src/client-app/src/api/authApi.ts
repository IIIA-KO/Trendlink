import axiosInstance from './Api.ts';
import { UserType } from '../models/UserType';
import { AuthResponseType } from '../models/AuthResponseType'

export const register = async (data: {
    firstName: string;
    lastName: string;
    birthDate: string;
    email: string;
    phoneNumber: string;
    password: string;
    stateId: string;
}): Promise<AuthResponseType> => {
    const response = await axiosInstance.post('/users/register', data);
    return {
        accessToken: response.data.accessToken,
        refreshToken: response.data.refreshToken,
        expiresIn: 900,
    };
};

export const login = async (credentials: UserType): Promise<AuthResponseType> => {
    const response = await axiosInstance.post('/users/login', credentials);
    return {
        accessToken: response.data.accessToken,
        refreshToken: response.data.refreshToken,
        expiresIn: 900,
    };
};

export const logout = async (): Promise<void> => {
    await axiosInstance.post('/users/logout');
};

export const refreshAccessToken = async (refreshToken: string): Promise<AuthResponseType> => {
    const response = await axiosInstance.post('/users/refresh', { refreshToken });
    return {
        accessToken: response.data.accessToken,
        refreshToken: response.data.refreshToken,
        expiresIn: 900,
    };
};