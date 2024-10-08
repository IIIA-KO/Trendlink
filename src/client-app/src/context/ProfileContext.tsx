import React, {createContext, ReactNode, useContext, useEffect, useState} from "react";
import {UserType} from "../types/UserType";
import {ProfileContextType} from "../types/ProfileContextType";
import {getUser} from "../services/user";

const ProfileContext = createContext<ProfileContextType | undefined>(undefined);

const ProfileProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    const [user, setUser] = useState<UserType | null>(null);
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        const fetchUserData = async () => {
            try {
                const userData = await getUser();
                setUser(userData);
            } catch (err) {
                setError('Не вдалося отримати дані користувача');
            } finally {
                setLoading(false);
            }
        };

        fetchUserData();
    }, []);

    return (
        <ProfileContext.Provider value={{ user, loading, error }}>
            {children}
        </ProfileContext.Provider>
    );
};

export { ProfileProvider, ProfileContext };