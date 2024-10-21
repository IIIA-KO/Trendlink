import {CalendarDayType} from "./CalendarDayType";

export interface CalendarContextType {
    calendarData: CalendarDayType[] | null;
    fetchCalendarData: () => Promise<void>;
    fetchCalendarByUserId: (userId: string) => Promise<void>;
    fetchMonthCalendar: () => Promise<void>;
    fetchMonthCalendarByUserId: (userId: string) => Promise<void>;
    blockSelectedDate: (date: string) => Promise<void>;
    unblockSelectedDate: (date: string) => Promise<void>;
}