import {useContext} from "react";
import {AdvertisementsContext} from "../context/AdvertisementsContext";

export const useAdvertisements = () => {
    const context = useContext(AdvertisementsContext);

    if (!context) {
        throw new Error('useAdvertisements must be used within a AdvertisementsProvider');
    }
    return context;
};