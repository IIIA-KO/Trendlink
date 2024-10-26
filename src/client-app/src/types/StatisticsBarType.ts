import {UserType} from "./UserType";
import {InstagramPostType} from "./InstagramPostType";
import {AdvertisementsAveragePriceType} from "./AdvertisementsAveragePriceType";

export interface StatisticsBarType {
    user: UserType | null ;
    posts: InstagramPostType[] | null;
    advertisements: AdvertisementsAveragePriceType[] | null;
}