import axiosInstance from "./api";
import { handleError } from "../utils/handleError";
import {ProfileType} from "../types/ProfileType";

export const deleteProfilePhoto = async (): Promise<void> => {
    try {
        await axiosInstance.delete('/profile/photo');
    } catch (error) {
        handleError(error);
    }
};

export const updateProfile = async (profileData: ProfileType): Promise<void> => {
    try {
        await axiosInstance.put('/profile', profileData);
    } catch (error) {
        handleError(error);
    }
};