import {UserType} from "./UserType";
import {UsersType} from "./UsersType";

export type UserContextType = {
    user: UserType | null;
    userByID: UserType | null;
    fetchUserData: () => void;
    fetchUserByID: (userId: string) => void;
    fetchUserByIDRE: (userId: string) => Promise<UserType | null>;
    fetchUsers: (params: UsersType) => Promise<UserType[] | null>;
};