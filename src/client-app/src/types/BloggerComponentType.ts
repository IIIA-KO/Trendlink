import {UserType} from "./UserType";
import {InstagramPostType} from "./InstagramPostType";
import {AdvertisementsAveragePriceType} from "./AdvertisementsAveragePriceType";

export interface BloggerComponentType {
    userID?: string;
    user: UserType | null ;
    posts: InstagramPostType[] | null;
    advertisements: AdvertisementsAveragePriceType[] | null;
}