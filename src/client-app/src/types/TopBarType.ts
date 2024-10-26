import {UserType} from "./UserType";

export interface TopBarType {
    user: UserType | null;
    showButton?: 'on' | 'off';
}