import {UserType} from "./UserType";
import {UsersType} from "./UsersType";

export type UserContextType = {
    user: UserType | null;
    fetchUserByID: (userId: string) => Promise<UserType | null>;
    fetchUsers: (params: UsersType) => Promise<UserType[] | null>;
};