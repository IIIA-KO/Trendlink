import React, {createContext, ReactNode, useEffect, useState} from "react";
import {UserType} from "../types/UserType";
import {ProfileContextType} from "../types/ProfileContextType";
import {getUser} from "../services/user";
import {handleError} from "../utils/handleError";
import {useNavigate} from "react-router-dom";
import {getAdvertisements} from "../services/advertisements";
import {AdvertisementsType} from "../types/AdvertisementsType";

const ProfileContext = createContext<ProfileContextType | undefined>(undefined);

const ProfileProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    const [user, setUser] = useState<UserType | null>(null);
    const [advertisements, setAdvertisements] = useState<AdvertisementsType | null>(null);
    const [loading, setLoading] = useState<boolean>(true);
    const navigate = useNavigate();

    useEffect(() => {
        const fetchUserData = async () => {
            try {
                const userData = await getUser();
                setUser(userData);
            } catch (error) {
                handleError(error)
            } finally {
                setLoading(false);
            }
        };

        const fetchAdvertisementsData = async () => {
            try {
                const advertisementsData = await getAdvertisements();
                setAdvertisements(advertisementsData);
            } catch (error) {
                handleError(error)
            } finally {
                setLoading(false);
            }
        };

        fetchUserData();
        fetchAdvertisementsData();
    }, []);

    if(loading === true) {
        navigate("/loading")
    }

    return (
        <ProfileContext.Provider value={{ user, advertisements }}>
            {children}
        </ProfileContext.Provider>
    );
};

export { ProfileProvider, ProfileContext };