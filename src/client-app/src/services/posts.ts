import axiosInstance from "./api";
import {handleError} from "../utils/handleError";
import {UserPostsResponse} from "../types/UserPostsResponseType";

export const getPosts = async (limit: number, cursorType: string | null = null, cursor: string | null = null): Promise<UserPostsResponse | null> => {
    try {
        let url = `/posts?limit=${limit}`;
        if (cursor && cursorType) {
            url += `&cursorType=${cursorType}&cursor=${cursor}`;
        }

        const response = await axiosInstance.get(url);
        return response.data;
    } catch (error) {
        handleError(error);
        return null;
    }
}

export const getPostsByID = async (postId: string): Promise<UserPostsResponse | null> => {
    try {
        const response = await axiosInstance.get(`/posts/${postId}`);
        return response.data;
    } catch (error) {
        handleError(error);
        return null;
    }
};