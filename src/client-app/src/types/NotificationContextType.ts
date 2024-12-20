import {NotificationType} from "./NotificationType";
import {NotificationQueryParams} from "./NotificationQueryParams";
import {CreateNotificationType} from "./CreateNotificationType";
import {UpdateNotificationType} from "./UpdateNotificationType";
import {PaginationHeaders} from "./PaginationHeadersType";

export interface NotificationContextType {
    notifications: NotificationType[] | null;
    fetchNotifications: (params?: NotificationQueryParams) => Promise<void>;
    createNewNotification: (notificationData: CreateNotificationType) => Promise<void>;
    updateExistingNotification: (id: string, notificationData: UpdateNotificationType) => Promise<void>;
    paginationData: PaginationHeaders;
}