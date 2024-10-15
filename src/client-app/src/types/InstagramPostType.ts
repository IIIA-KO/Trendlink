import {InstagramInsightType} from "./InstagramInsightType";

export type InstagramPostType = {
    id: string;
    mediaType: string;
    mediaUrl: string;
    permalink: string;
    thumbnailUrl: string | null;
    timestamp: string;
    insights: InstagramInsightType[];
};