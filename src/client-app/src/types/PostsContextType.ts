import {InstagramPostType} from "./InstagramPostType";

export type PostsContextType = {
    posts: InstagramPostType[] | null;
    hasNextPage: boolean;
    hasPreviousPage: boolean;
    fetchPosts: (cursorType?: string | null, cursor?: string | null) => void;
    fetchPostsByID: (userId: string, limit?: number, cursorType?: string | null, cursor?: string | null) => void;
    afterCursor: string | null;
    beforeCursor: string | null;
    loading: boolean;
};