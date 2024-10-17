import React, {createContext, ReactNode, useEffect, useState} from "react";
import {UserType} from "../types/UserType";
import {ProfileContextType} from "../types/ProfileContextType";
import {getUser} from "../services/user";
import {handleError} from "../utils/handleError";
import {getAdvertisements} from "../services/advertisements";
import {AdvertisementsType} from "../types/AdvertisementsType";
import {InstagramPostType} from "../types/InstagramPostType";
import {getPosts} from "../services/posts";
import {getAudienceGenderPercentage} from "../services/audiences";
import {AudienceGenderData} from "../types/AudienceGenderDataType";

const ProfileContext = createContext<ProfileContextType | undefined>(undefined);

const ProfileProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    const [user, setUser] = useState<UserType | null>(null);
    const [advertisements, setAdvertisements] = useState<AdvertisementsType[] | null>(null);
    const [posts, setPosts] = useState<InstagramPostType[] | null>(null);
    const [loading, setLoading] = useState<boolean>(true);
    const [genderData, setGenderData] = useState<AudienceGenderData | null>(null);
    const [hasNextPage, setHasNextPage] = useState<boolean>(false);
    const [hasPreviousPage, setHasPreviousPage] = useState<boolean>(false);
    const [afterCursor, setAfterCursor] = useState<string | null>(null);
    const [beforeCursor, setBeforeCursor] = useState<string | null>(null);

    const fetchUserData = async () => {
        try {
            const userData = await getUser();
            setUser(userData);
        } catch (error) {
            handleError(error)
        } finally {
            setLoading(false);
        }
    };

    const fetchAdvertisementsData = async () => {
        try {
            const advertisementsData = await getAdvertisements();
            setAdvertisements(advertisementsData);
        } catch (error) {
            handleError(error)
            setAdvertisements(new Array<AdvertisementsType>());
        } finally {
            setLoading(false);
        }
    };

    const fetchPosts = async (cursorType: string | null = null, cursor: string | null = null) => {
        try {
            setLoading(true);
            const postsData = await getPosts(6, cursorType, cursor);
            setPosts(postsData ? postsData.posts : null);

            if (postsData?.paging) {
                setAfterCursor(postsData.paging.after ?? null);
                setBeforeCursor(postsData.paging.before ?? null);
                setHasNextPage(!!postsData.paging.after);
                setHasPreviousPage(!!postsData.paging.before);
            } else {
                setHasNextPage(false);
                setHasPreviousPage(false);
            }
        } catch (error) {
            handleError(error);
        } finally {
            setLoading(false);
        }
    };

    const fetchAudienceGenderPercentage = async () => {
        try {
            const genderDataResponse = await getAudienceGenderPercentage();
            setGenderData(genderDataResponse);
        } catch (error) {
            handleError(error);
        }
    };

    useEffect(() => {
        fetchUserData();
        fetchAdvertisementsData();
        fetchAudienceGenderPercentage();
        fetchPosts();
    }, []);

    return (
        <ProfileContext.Provider
            value={{
                user,
                advertisements,
                posts,
                genderData,
                loading,
                hasNextPage,
                hasPreviousPage,
                afterCursor,
                beforeCursor,
                fetchPosts,
                fetchAudienceGenderPercentage,
        }}>
            {children}
        </ProfileContext.Provider>
    );
};

export { ProfileProvider, ProfileContext };