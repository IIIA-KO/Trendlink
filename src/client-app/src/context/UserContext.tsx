import React, {createContext, ReactNode, useEffect, useState} from "react";
import {UserType} from "../types/UserType";
import {getUser, getUserByID, getUsers} from "../services/user";
import {handleError} from "../utils/handleError";
import {UserContextType} from "../types/UserContextType";
import {UsersType} from "../types/UsersType";
import {PaginationHeaders} from "../types/PaginationHeadersType";

const UserContext = createContext<UserContextType | undefined>(undefined);

const UserProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    const [user, setUser] = useState<UserType | null>(null);
    const [userByID, setUserByID] = useState<UserType | null>(null);
    const [paginationData, setPaginationData] = useState<PaginationHeaders>({
        currentPage: 1,
        itemsPerPage: 10,
        totalItems: 0,
        totalPages: 0,
        hasNextPage: false,
        hasPreviousPage: false,
    });

    const fetchUserData = async () => {
        try {
            const userData = await getUser();
            setUser(userData);
        } catch (error) {
            handleError(error)
            return null;
        }
    };

    const fetchUserByID = async (userId: string) => {
        try {
            const userData = await getUserByID(userId);
            setUserByID(userData);
        } catch (error) {
            handleError(error);
            return null;
        }
    };

    const fetchUserByIDRE = async (userId: string): Promise<UserType | null> => {
        try {
            return await getUserByID(userId);
        } catch (error) {
            handleError(error);
            return null;
        }
    };

    const fetchUsers = async (params: UsersType): Promise<UserType[] | null> => {
        try {
            const response = await getUsers(params);
            if (response) {
                setPaginationData(response.pagination);
                return response.data;
            }
            return null;
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
                userByID,
                fetchUserData,
                fetchUserByID,
                fetchUsers,
                fetchUserByIDRE
        }}>
            {children}
        </UserContext.Provider>
    );
};

export { UserProvider, UserContext };