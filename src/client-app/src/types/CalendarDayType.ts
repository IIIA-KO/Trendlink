import {CooperationType} from "./CooperationType";

export interface CalendarDayType {
    date: string;
    isBlocked: boolean;
    cooperations: CooperationType[];
}