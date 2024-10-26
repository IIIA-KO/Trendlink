import {UserType} from "../types/UserType";
import axiosInstance from "./api";
import {handleError} from "../utils/handleError";
import {UsersType} from "../types/UsersType";
import {PaginationHeaders} from "../types/PaginationHeadersType";

export const getUser = async (): Promise<UserType | null> => {
    try {
        const response = await axiosInstance.get('/users/me');
        return response.data;
    } catch (error) {
        handleError(error);
        return null;
    }
};

export const getUsers = async (params: Partial<UsersType>): Promise<{ data: UserType[]; pagination: PaginationHeaders } | null> => {
    try {
        const response = await axiosInstance.get('/users', { params });
        if (response) {
            const data = response.data;
            const pagination: PaginationHeaders = JSON.parse(response.headers.pagination);

            return { data, pagination };
        }

        return null;
    } catch (error) {
        handleError(error);
        return null;
    }
};

export const getUserByID = async (userId: string): Promise<UserType | null> => {
    try {
        const response = await axiosInstance.get(`/users/${userId}`);
        return response.data;
    } catch (error) {
        handleError(error);
        return null;
    }
};