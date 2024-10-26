import axiosInstance from "./api";
import { handleError } from "../utils/handleError";
import {  CooperationType } from "../types/CooperationType";
import {RequestCooperationType} from "../types/RequestCooperationType";


export const requestCooperation = async (cooperationData: RequestCooperationType): Promise<CooperationType | null> => {
    try {
        const response = await axiosInstance.post('/cooperations/request-cooperation', cooperationData);
        return response.data;
    } catch (error) {
        handleError(error);
        return null;
    }
};

export const confirmCooperation = async (id: string): Promise<void> => {
    try {
        await axiosInstance.post(`/cooperations/${id}/confirm`);
    } catch (error) {
        handleError(error);
    }
};

export const rejectCooperation = async (id: string): Promise<void> => {
    try {
        await axiosInstance.post(`/cooperations/${id}/reject`);
    } catch (error) {
        handleError(error);
    }
};

export const cancelCooperation = async (id: string): Promise<void> => {
    try {
        await axiosInstance.post(`/cooperations/${id}/cancel`);
    } catch (error) {
        handleError(error);
    }
};

export const markCooperationAsDone = async (id: string): Promise<void> => {
    try {
        await axiosInstance.post(`/cooperations/${id}/mark-as-done`);
    } catch (error) {
        handleError(error);
    }
};

export const completeCooperation = async (id: string): Promise<void> => {
    try {
        await axiosInstance.post(`/cooperations/${id}/complete`);
    } catch (error) {
        handleError(error);
    }
};