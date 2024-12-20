import React, {createContext, ReactNode, useEffect, useState} from "react";
import {handleError} from "../utils/handleError";
import {InstagramPostType} from "../types/InstagramPostType";
import {getPosts, getPostsByID} from "../services/posts";
import {PostsContextType} from "../types/PostsContextType";

const PostsContext = createContext<PostsContextType | undefined>(undefined);

const PostsProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    const [posts, setPosts] = useState<InstagramPostType[] | null>(null);
    const [postsByID, setPostsByID] = useState<InstagramPostType[] | null>(null);
    const [hasNextPage, setHasNextPage] = useState<boolean>(false);
    const [hasPreviousPage, setHasPreviousPage] = useState<boolean>(false);
    const [afterCursor, setAfterCursor] = useState<string | null>(null);
    const [beforeCursor, setBeforeCursor] = useState<string | null>(null);
    const [loading, setLoading] = useState<boolean>(true);

    const fetchPosts = async (cursorType: string | null = null, cursor: string | null = null) => {
        try {
            const postsData = await getPosts(6, cursorType, cursor);

            if (postsData) {
                setPosts(postsData.posts);
                setAfterCursor(postsData.paging?.after ?? null);
                setBeforeCursor(postsData.paging?.before ?? null);
                setHasNextPage(!!postsData.paging?.nextCursor);
                setHasPreviousPage(!!postsData.paging?.previousCursor);
            } else {
                setPosts(null);
                setHasNextPage(false);
                setHasPreviousPage(false);
            }
        } catch (error) {
            handleError(error);
        } finally {
            setLoading(false);
        }
    };

    const fetchPostsByID = async (
        userId: string,
        limit: number = 6,
        cursorType: string | null = null,
        cursor: string | null = null
    ) => {
        setLoading(true);
        try {
            const postsData = await getPostsByID(userId, limit, cursorType, cursor);
            if (postsData) {
                setPostsByID(postsData.posts);
                setAfterCursor(postsData.paging?.after ?? null);
                setBeforeCursor(postsData.paging?.before ?? null);
                setHasNextPage(!!postsData.paging?.after);
                setHasPreviousPage(!!postsData.paging?.before);
            } else {
                setPosts(null);
                setHasNextPage(false);
                setHasPreviousPage(false);
            }
        } catch (error) {
            handleError(error);
        } finally {
            setLoading(false);
        }
    };


    useEffect(() => {
        fetchPosts();
    }, []);

    return (
        <PostsContext.Provider
            value={{
                posts,
                hasNextPage,
                hasPreviousPage,
                afterCursor,
                beforeCursor,
                fetchPosts,
                fetchPostsByID,
                loading
            }}>
            {children}
        </PostsContext.Provider>
    );
};

export { PostsProvider, PostsContext };