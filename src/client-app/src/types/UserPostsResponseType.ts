import {InstagramPostType} from "./InstagramPostType";
import {InstagramPagingType} from "./InstagramPagingType";

export type UserPostsResponse = {
    posts: InstagramPostType[];
    paging: InstagramPagingType;
};