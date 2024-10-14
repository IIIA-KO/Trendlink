import {UserType} from "../types/UserType";
import axiosInstance from "./api";
import {handleError} from "../utils/handleError";
import {AdvertisementsType} from "../types/AdvertisementsType";

export const getAdvertisements = async (): Promise<AdvertisementsType | null> => {
    try {
        const response = await axiosInstance.get('/advertisements/avarage-prices');
        return response.data;
    } catch (error) {
        handleError(error);
        return null;
    }
};

export const getAdvertisementsByID = async (query: string): Promise<AdvertisementsType | null> => {
    try {
        const response = await axiosInstance.get(`/advertisements/avarage-prices/${query}`);
        return response.data;
    } catch (error) {
        handleError(error);
        return null;
    }
};