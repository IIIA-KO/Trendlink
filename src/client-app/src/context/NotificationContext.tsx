import React, { createContext, ReactNode, useEffect, useState } from "react";
import { getNotifications, createNotification, updateNotification } from "../services/notifications";
import { NotificationType } from "../types/NotificationType";
import {NotificationQueryParams} from "../types/NotificationQueryParams";
import {CreateNotificationType} from "../types/CreateNotificationType";
import {UpdateNotificationType} from "../types/UpdateNotificationType";
import { handleError } from "../utils/handleError";
import {NotificationContextType} from "../types/NotificationContextType";
import {PaginationHeaders} from "../types/PaginationHeadersType";

const NotificationContext = createContext<NotificationContextType | undefined>(undefined);

const NotificationProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    const [notifications, setNotifications] = useState<NotificationType[] | null>(null);
    const [paginationData, setPaginationData] = useState<PaginationHeaders>({
        currentPage: 1,
        itemsPerPage: 10,
        totalItems: 0,
        totalPages: 0,
        hasNextPage: false,
        hasPreviousPage: false,
    });

    const fetchNotifications = async (params: NotificationQueryParams = { pageNumber: 1, pageSize: 10 }) => {
        try {
            const response = await getNotifications(params);
            if (response) {
                setNotifications(response.data);
                setPaginationData(response.pagination);
            }
        } catch (error) {
            handleError(error);
        }
    };

    const createNewNotification = async (notificationData: CreateNotificationType) => {
        try {
            await createNotification(notificationData);
            fetchNotifications({ pageNumber: 1, pageSize: 10 }); // Передаем параметры по умолчанию
        } catch (error) {
            handleError(error);
        }
    };

    const updateExistingNotification = async (id: string, notificationData: UpdateNotificationType) => {
        try {
            await updateNotification(id, notificationData);
            fetchNotifications({ pageNumber: 1, pageSize: 10 }); // Передаем параметры по умолчанию
        } catch (error) {
            handleError(error);
        }
    };

    useEffect(() => {
        fetchNotifications(); // Передаем параметры по умолчанию
    }, []);

    return (
        <NotificationContext.Provider
            value={{
                notifications,
                fetchNotifications,
                createNewNotification,
                updateExistingNotification,
                paginationData, // Передаем данные пагинации в контекст
            }}
        >
            {children}
        </NotificationContext.Provider>
    );
};


export { NotificationProvider, NotificationContext };
