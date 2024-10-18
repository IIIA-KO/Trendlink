import axiosInstance from "./api";
import {handleError} from "../utils/handleError";
import {AdvertisementsAveragePriceType} from "../types/AdvertisementsAveragePriceType";
import {AdvertisementsType} from "../types/AdvertisementsType";

export type PartialAdvertisementsType = Partial<AdvertisementsType>;

export const getAdvertisements = async (): Promise<AdvertisementsAveragePriceType[] | null> => {
    try {
        const response = await axiosInstance.get('/advertisements/avarage-prices');
        return response.data;
    } catch (error) {
        handleError(error);
        return null;
    }
};

export const getAdvertisementsByID = async (query: string): Promise<AdvertisementsAveragePriceType[] | null> => {
    try {
        const response = await axiosInstance.get(`/advertisements/avarage-prices/${query}`);
        return response.data;
    } catch (error) {
        handleError(error);
        return null;
    }
};

export const updateAdvertisement = async (id: string, data: PartialAdvertisementsType) => {
    try {
        const response = await axiosInstance.put(`/advertisements/ad/${id}/edit`, data);
        return response.data;
    } catch (error) {
        handleError(error);
        throw error;
    }
};

export const createAdvertisement = async (data: AdvertisementsType) => {
    try {
        const response = await axiosInstance.post(`/advertisements/ad`, data);
        return response.data;
    } catch (error) {
        handleError(error);
        throw error;
    }
};

export const deleteAdvertisement = async (id: string): Promise<void> => {
    try {
        await axiosInstance.delete(`/advertisements/ad/${id}/delete`);
    } catch (error) {
        handleError(error);
        throw error;
    }
};