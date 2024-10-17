import { handleError } from "../utils/handleError";
import axiosInstance from "./api";

export const linkInstagram = async (code: string): Promise<boolean> => {
    try {
        const response = await axiosInstance.post('/intstagram/link-account', { code });
        return response.status === 200;
    } catch (error) {
        handleError(error);
        return false;
    }
}

export const renewInstagramAccess = async (code: string): Promise<boolean> => {
    try {
        const response = await axiosInstance.post('/instagram/renew-access', { code });
        return response.status === 200;
    } catch (error) {
        handleError(error);
        return false;
    }
}