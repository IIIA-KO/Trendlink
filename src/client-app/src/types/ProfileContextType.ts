import {UserType} from "./UserType";
import {AdvertisementsAveragePriceType} from "./AdvertisementsAveragePriceType";
import {InstagramPostType} from "./InstagramPostType";
import {AudienceGenderData} from "./AudienceGenderDataType";

export type ProfileContextType = {
    user: UserType | null;
    advertisements: AdvertisementsAveragePriceType[] | null;
    posts: InstagramPostType[] | null;
    loading: boolean;
    hasNextPage: boolean;
    hasPreviousPage: boolean;
    fetchPosts: (cursorType?: string | null, cursor?: string | null) => void;
    afterCursor: string | null;
    beforeCursor: string | null;
    genderData: AudienceGenderData | null;
    fetchAudienceGenderPercentage: () => void;
};