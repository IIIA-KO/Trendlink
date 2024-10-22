import {CalendarContextType} from "../types/CalendarContextType";
import {createContext, ReactNode, useEffect, useState} from "react";
import {CalendarDayType} from "../types/CalendarDayType";
import {
    blockDate,
    getCalendar,
    getCalendarByUserId,
    getMonthCalendar,
    getMonthCalendarByUserId, unblockDate
} from "../services/calendar";
import {handleError} from "../utils/handleError";

const CalendarContext = createContext<CalendarContextType | undefined>(undefined);

const CalendarProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    const [calendarData, setCalendarData] = useState<CalendarDayType[] | null>(null);

    const fetchCalendarData = async () => {
        try {
            const data: CalendarDayType[] = await getCalendar();
            setCalendarData(data);
        } catch (error) {
            handleError(error);
        }
    };

    const fetchCalendarByUserId = async (userId: string) => {
        try {
            const data: CalendarDayType[] = await getCalendarByUserId(userId);
            setCalendarData(data);
        } catch (error) {
            handleError(error);
        }
    };

    const fetchMonthCalendar = async (month: string, year: string) => {
        try {
            const data: CalendarDayType[] = await getMonthCalendar(month, year);
            setCalendarData(data);
        } catch (error) {
            handleError(error);
        }
    };

    const fetchMonthCalendarByUserId = async (userId: string) => {
        try {
            const data: CalendarDayType[] = await getMonthCalendarByUserId(userId);
            setCalendarData(data);
        } catch (error) {
            handleError(error);
        }
    };

    const blockSelectedDate = async (date: string) => {
        try {
            await blockDate(date);
            fetchCalendarData();
        } catch (error) {
            handleError(error);
        }
    };

    const unblockSelectedDate = async (date: string) => {
        try {
            await unblockDate(date);
            fetchCalendarData();
        } catch (error) {
            handleError(error);
        }
    };

    useEffect(() => {
        fetchCalendarData();
    }, []);

    return (
        <CalendarContext.Provider
            value={{
                calendarData,
                fetchCalendarData,
                fetchCalendarByUserId,
                fetchMonthCalendar,
                fetchMonthCalendarByUserId,
                blockSelectedDate,
                unblockSelectedDate
            }}
        >
            {children}
        </CalendarContext.Provider>
    );
};

export { CalendarProvider, CalendarContext };