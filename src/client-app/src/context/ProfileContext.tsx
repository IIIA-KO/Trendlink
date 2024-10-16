import React, {createContext, ReactNode, useEffect, useState} from "react";
import {UserType} from "../types/UserType";
import {ProfileContextType} from "../types/ProfileContextType";
import {getUser} from "../services/user";
import {handleError} from "../utils/handleError";
import {getAdvertisements} from "../services/advertisements";
import {AdvertisementsType} from "../types/AdvertisementsType";
import {InstagramPostType} from "../types/InstagramPostType";
import {getPosts} from "../services/posts";

const ProfileContext = createContext<ProfileContextType | undefined>(undefined);

const ProfileProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    const [user, setUser] = useState<UserType | null>(null);
    const [advertisements, setAdvertisements] = useState<AdvertisementsType | null>(null);
    const [posts, setPosts] = useState<InstagramPostType[] | null>(null);
    const [loading, setLoading] = useState<boolean>(true);

    useEffect(() => {
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
            } finally {
                setLoading(false);
            }
        };

        const fetchPosts = async () => {
            try {
                const postsData = await getPosts(6);
                setPosts(postsData ? postsData.posts : null);
            } catch (error) {
                handleError(error);
            }
        };

        fetchUserData();
        fetchAdvertisementsData();
        fetchPosts();
    }, []);

    return (
        <ProfileContext.Provider value={{ user, advertisements, posts, loading }}>
            {children}
        </ProfileContext.Provider>
    );
};

export { ProfileProvider, ProfileContext };