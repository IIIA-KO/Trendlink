import {UserType} from "./UserType";
import {AdvertisementsType} from "./AdvertisementsType";

export type ProfileContextType = {
    user: UserType | null;
    advertisements: AdvertisementsType | null;
};