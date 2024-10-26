import {AdvertisementsAveragePriceType} from "./AdvertisementsAveragePriceType";

export type AdvertisementsContextType = {
    advertisements: AdvertisementsAveragePriceType[] | null;
    advertisementByID: AdvertisementsAveragePriceType[] | null;
    fetchAdvertisementsData: () => void;
    fetchAdvertisementsByID: (id: string) => void;
};