import React, {createContext, ReactNode, useEffect, useState} from "react";
import {UserType} from "../types/UserType";
import {getUser, getUserByID, getUsers} from "../services/user";
import {handleError} from "../utils/handleError";
import {UserContextType} from "../types/UserContextType";
import {UsersType} from "../types/UsersType";

const UserContext = createContext<UserContextType | undefined>(undefined);

const UserProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    const [user, setUser] = useState<UserType | null>(null);

    const fetchUserData = async () => {
        try {
            const userData = await getUser();
            setUser(userData);
        } catch (error) {
            handleError(error)
        }
    };

    const fetchUserByID = async (userId: string): Promise<UserType | null> => {
        try {
            return await getUserByID(userId);
        } catch (error) {
            handleError(error);
            return null;
        }
    };

    const fetchUsers = async (params: UsersType): Promise<UserType[] | null> => {
        try {
            return await getUsers(params);
        } catch (error) {
            handleError(error);
            return null;
        }
    };

    useEffect(() => {
        fetchUserData();
    }, []);

    return (
        <UserContext.Provider
            value={{
                user,
                fetchUserByID,
                fetchUsers,
        }}>
            {children}
        </UserContext.Provider>
    );
};

export { UserProvider, UserContext };