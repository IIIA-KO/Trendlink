import { handleError } from "../utils/handleError";
import axiosInstance from "./api";

export const linkInstagram = async (code: string): Promise<boolean> => {
    try {
        const accessToken = localStorage.getItem('accessToken');
        
        const response = await fetch('https://localhost:5001/api/instagram/link-account', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + accessToken
            },
            body: JSON.stringify({
                code: code,
            }),
        });

        console.log("Response instagram link:", response);
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