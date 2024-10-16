import {UserType} from "./UserType";
import {AdvertisementsType} from "./AdvertisementsType";
import {InstagramPostType} from "./InstagramPostType";

export type ProfileContextType = {
    user: UserType | null;
    advertisements: AdvertisementsType | null;
    posts: InstagramPostType[] | null;
    loading: boolean;
};