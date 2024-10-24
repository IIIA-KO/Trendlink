import axiosInstance from "./api";
import { handleError } from "../utils/handleError";
import { CalendarDayType } from "../types/CalendarDayType";

export const getCalendar = async (): Promise<CalendarDayType[]> => {
    try {
        const response = await axiosInstance.get('/calendar');
        return response.data;
    } catch (error) {
        handleError(error);
        return [];
    }
};

export const getCalendarByUserId = async (userId: string): Promise<CalendarDayType[]> => {
    try {
        const response = await axiosInstance.get(`/calendar/${userId}`);
        return response.data;
    } catch (error) {
        handleError(error);
        return [];
    }
};

export const getMonthCalendar = async (month: string, year: string): Promise<CalendarDayType[]> => {
    try {
        const response = await axiosInstance.get('/calendar/month');
        return response.data;
    } catch (error) {
        handleError(error);
        return [];
    }
};

export const getMonthCalendarByUserId = async (userId: string): Promise<CalendarDayType[]> => {
    try {
        const response = await axiosInstance.get(`/calendar/month/${userId}`);
        return response.data;
    } catch (error) {
        handleError(error);
        return [];
    }
};

export const blockDate = async (date: string): Promise<void> => {
    try {
        await axiosInstance.post('/calendar/block-date', { date });
    } catch (error) {
        handleError(error);
    }
};

export const unblockDate = async (date: string): Promise<void> => {
    try {
        await axiosInstance.delete('/calendar/unblock-date', {
            data: { date },
        });
    } catch (error) {
        handleError(error);
    }
};