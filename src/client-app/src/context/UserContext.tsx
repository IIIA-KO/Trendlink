import React, {createContext, ReactNode, useEffect, useState} from "react";
import {UserType} from "../types/UserType";
import {getUser} from "../services/user";
import {handleError} from "../utils/handleError";
import {UserContextType} from "../types/UserContextType";

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

    useEffect(() => {
        fetchUserData();
    }, []);

    return (
        <UserContext.Provider
            value={{
                user
        }}>
            {children}
        </UserContext.Provider>
    );
};

export { UserProvider, UserContext };