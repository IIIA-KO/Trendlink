import {handleError} from "../utils/handleError";
import {AuthResponseType} from "../types/AuthResponseType";
import GoogleRegisterType from "../types/GoogleRegisterType";
import GoogleLoginType from "../types/GoogleLoginType";
import axiosInstance from "./api";

export const loginWithGoogle = async (credentials: GoogleLoginType): Promise<AuthResponseType | null> => {
    try {
        console.log('Sending login request to /accounts/google-login with code:', credentials.code);
        const response = await axiosInstance.post('/accounts/google-login', credentials);
        console.log('Login response:', response.data);
        console.log('Access Token:', response.data.accessToken);
        return {
            accessToken: response.data.accessToken,
            refreshToken: response.data.refreshToken,
            expiresIn: response.data.expiresIn,
        };
    } catch (error) {
        handleError(error);
        return null;
    }
};

export const registerWithGoogle = async (credentials: GoogleRegisterType): Promise<AuthResponseType | null> => {
    try {
        const response = await axiosInstance.post('/accounts/google-register', credentials);
        return {
            accessToken: response.data.accessToken,
            refreshToken: response.data.refreshToken,
            expiresIn: response.data.expiresIn,
        };
    } catch (error) {
        handleError(error);
        return null;
    }
};