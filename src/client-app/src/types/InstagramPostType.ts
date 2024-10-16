import {InstagramInsightType} from "./InstagramInsightType";

export type InstagramPostType = {
    id: string;
    mediaType: 'IMAGE' | 'VIDEO' | 'CAROUSEL_ALBUM';
    mediaUrl: string;
    permalink: string;
    thumbnailUrl: string | null;
    timestamp: string;
    insights: InstagramInsightType[];
};