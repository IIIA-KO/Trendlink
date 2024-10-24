export interface NotificationType {
    id: string;
    userId: string;
    notificationType: number;
    title: string;
    message: string;
    isRead: boolean;
    createdOnUtc: string;
}