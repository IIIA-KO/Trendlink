import {AdvertisementsType} from "./AdvertisementsType";

export interface TermsAndConditionsType {
    id: string;
    userId: string;
    description: string;
    advertisements: AdvertisementsType[];
}