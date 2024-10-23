import axiosInstance from "./api";
import { handleError } from "../utils/handleError";
import { NotificationType} from "../types/NotificationType";
import {NotificationQueryParams} from "../types/NotificationQueryParams";
import {CreateNotificationType} from "../types/CreateNotificationType";
import {UpdateNotificationType} from "../types/UpdateNotificationType";

export const getNotifications = async (params?: NotificationQueryParams): Promise<NotificationType[] | null> => {
    try {
        const response = await axiosInstance.get('/notifications', { params });
        return response.data;
    } catch (error) {
        handleError(error);
        return null;
    }
};

export const createNotification = async (notificationData: CreateNotificationType): Promise<NotificationType | null> => {
    try {
        const response = await axiosInstance.post('/notifications', notificationData);
        return response.data;
    } catch (error) {
        handleError(error);
        return null;
    }
};

export const updateNotification = async (id: string, notificationData: UpdateNotificationType): Promise<NotificationType | null> => {
    try {
        const response = await axiosInstance.put(`/notifications/${id}`, notificationData);
        return response.data;
    } catch (error) {
        handleError(error);
        return null;
    }
};
