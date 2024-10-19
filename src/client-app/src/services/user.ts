import {UserType} from "../types/UserType";
import axiosInstance from "./api";
import {handleError} from "../utils/handleError";
import {UsersType} from "../types/UsersType";

export const getUser = async (): Promise<UserType | null> => {
    try {
        const response = await axiosInstance.get('/users/me');
        return response.data;
    } catch (error) {
        handleError(error);
        return null;
    }
};

export const getUsers = async (params: UsersType): Promise<UserType[] | null> => {
    try {
        const response = await axiosInstance.get('/users', { params });
        return response.data;
    } catch (error) {
        handleError(error);
        return null;
    }
};