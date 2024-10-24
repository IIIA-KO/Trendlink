import React, { createContext, ReactNode, useEffect, useState } from "react";
import { getNotifications, createNotification, updateNotification } from "../services/notifications";
import { NotificationType } from "../types/NotificationType";
import {NotificationQueryParams} from "../types/NotificationQueryParams";
import {CreateNotificationType} from "../types/CreateNotificationType";
import {UpdateNotificationType} from "../types/UpdateNotificationType";
import { handleError } from "../utils/handleError";
import {NotificationContextType} from "../types/NotificationContextType";

const NotificationContext = createContext<NotificationContextType | undefined>(undefined);

const NotificationProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    const [notifications, setNotifications] = useState<NotificationType[] | null>(null);

    const fetchNotifications = async (params?: NotificationQueryParams) => {
        try {
            const data = await getNotifications(params);
            setNotifications(data);
        } catch (error) {
            handleError(error);
        }
    };

    const createNewNotification = async (notificationData: CreateNotificationType) => {
        try {
            await createNotification(notificationData);
            fetchNotifications();
        } catch (error) {
            handleError(error);
        }
    };

    const updateExistingNotification = async (id: string, notificationData: UpdateNotificationType) => {
        try {
            await updateNotification(id, notificationData);
            fetchNotifications();
        } catch (error) {
            handleError(error);
        }
    };

    useEffect(() => {
        fetchNotifications();
    }, []);

    return (
        <NotificationContext.Provider
            value={{
                notifications,
                fetchNotifications,
                createNewNotification,
                updateExistingNotification,
            }}
        >
            {children}
        </NotificationContext.Provider>
    );
};

export { NotificationProvider, NotificationContext };
