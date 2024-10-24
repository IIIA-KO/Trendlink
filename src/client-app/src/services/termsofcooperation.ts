import {handleError} from "../utils/handleError";
import {TermsAndConditionsType} from "../types/TermsAndConditionsType";
import axiosInstance from "./api";

export const getTermsAndConditions = async (): Promise<TermsAndConditionsType | null> => {
    try {
        const response = await axiosInstance.get('/terms-and-conditions');
        return response.data;
    } catch (error) {
        handleError(error);
        return null;
    }
};

export const createTermsAndConditions = async (data: Partial<TermsAndConditionsType>): Promise<TermsAndConditionsType | null> => {
    try {
        const response = await axiosInstance.post('/terms-and-conditions', data);
        return response.data;
    } catch (error) {
        handleError(error);
        return null;
    }
};

export const updateTermsAndConditions = async (data: Partial<TermsAndConditionsType>): Promise<TermsAndConditionsType | null> => {
    try {
        const response = await axiosInstance.put(`/terms-and-conditions`, data);
        return response.data;
    } catch (error) {
        handleError(error);
        return null;
    }
};