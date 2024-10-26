import React, {createContext, ReactNode, useEffect, useState} from "react";
import {handleError} from "../utils/handleError";
import {getAdvertisements, getAdvertisementsByID} from "../services/advertisements";
import {AdvertisementsAveragePriceType} from "../types/AdvertisementsAveragePriceType";
import {AdvertisementsContextType} from "../types/AdvertisementsContextType";

const AdvertisementsContext = createContext<AdvertisementsContextType | undefined>(undefined);

const AdvertisementsProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    const [advertisements, setAdvertisements] = useState<AdvertisementsAveragePriceType[] | null>(null);
    const [advertisementByID, setAdvertisementByID] = useState<AdvertisementsAveragePriceType[] | null>(null);

    const fetchAdvertisementsData = async () => {
        try {
            const advertisementsData = await getAdvertisements();
            setAdvertisements(advertisementsData);
        } catch (error) {
            handleError(error)
            setAdvertisements(new Array<AdvertisementsAveragePriceType>());
        }
    };

    const fetchAdvertisementsByID = async (id: string) => {
        try {
            const advertisementData = await getAdvertisementsByID(id);
            setAdvertisementByID(advertisementData);
        } catch (error) {
            handleError(error);
            setAdvertisementByID(new Array<AdvertisementsAveragePriceType>());
        }
    };

    useEffect(() => {
        fetchAdvertisementsData();
    }, []);

    return (
        <AdvertisementsContext.Provider
            value={{
                advertisements,
                advertisementByID,
                fetchAdvertisementsData,
                fetchAdvertisementsByID,
            }}>
            {children}
        </AdvertisementsContext.Provider>
    );
};

export { AdvertisementsProvider, AdvertisementsContext };