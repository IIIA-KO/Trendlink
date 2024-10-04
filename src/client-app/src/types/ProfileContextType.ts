import {UserType} from "./UserType";

export type ProfileContextType = {
    user: UserType | null;
    loading: boolean;
    error: string | null;
};