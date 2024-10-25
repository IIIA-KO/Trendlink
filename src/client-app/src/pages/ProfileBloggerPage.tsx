import TopBar from "../components/TopBar";
import StatisticsBar from "../components/StatisticsBar";
import like from "../assets/icons/Fill.svg"
import save from "../assets/icons/save-icon.svg"
import view from "../assets/icons/views-icon.svg"
import right from "../assets/icons/navigation-chevron-right.svg"
import left from "../assets/icons/navigation-chevron-left.svg"
import {getUserByID} from "../services/user";
import {useParams} from "react-router-dom";
import React, {useEffect, useState} from "react";
import {UserType} from "../types/UserType";
import {getPosts, getPostsByID} from "../services/posts";
import {InstagramPostType} from "../types/InstagramPostType";
import {getAdvertisements, getAdvertisementsByID} from "../services/advertisements";
import {AdvertisementsAveragePriceType} from "../types/AdvertisementsAveragePriceType";
import {usePosts} from "../hooks/usePosts";
import {useAdvertisements} from "../hooks/useAdvertisements";
import {useUser} from "../hooks/useUser";

const ProfileBloggerPage: React.FC = () => {
    const { userId } = useParams<{ userId: string }>();
    const { userByID, fetchUserByID } = useUser();
    const { advertisementByID, fetchAdvertisementsByID } = useAdvertisements();
    const {
        posts,
        hasNextPage,
        hasPreviousPage,
        afterCursor,
        beforeCursor,
        fetchPostsByID,
    } = usePosts();
    const [loading, setLoading] = useState(true);
    const [activeButton, setActiveButton] = useState<string>('Views');


    useEffect(() => {
        if (userId) {
            fetchUserByID(userId);
            fetchAdvertisementsByID(userId);
            fetchPostsByID(userId);
        }
    }, [userId, fetchUserByID, fetchAdvertisementsByID, fetchPostsByID]);


    const handleButtonClick = (buttonName: string) => {
        setActiveButton(buttonName);
    };

    return (
        <div
            className="flex flex-col gap-2 bg-custom-bg bg-cover bg-no-repeat rounded-[50px] h-auto w-auto min-h-screen min-w-screen sm:mr-24 md:mr-32 lg:mr-42 xl:mr-64 mt-10">
            <TopBar user={userByID} showButton={"off"}/>
            <div className="w-full h-full flex justify-center items-center mb-12">
                <div className="flex space-x-2">
                    <button
                        className={`border-b-[2px] border-gray-10 px-32 ${
                            activeButton === 'Views' ? 'border-main-black' : 'text-black'
                        }`}
                        onClick={() => handleButtonClick('Views')}
                    >
                        Views
                    </button>
                    <button
                        className={`border-b-[2px] border-gray-10 px-32  py-2 ${
                            activeButton === 'Reviews' ? 'border-main-black' : 'text-black'
                        }`}
                        onClick={() => handleButtonClick('Reviews')}
                    >
                        Reviews
                    </button>
                    <button
                        className={`border-b-[2px] border-gray-10 px-32  py-2 ${
                            activeButton === 'Statistics' ? 'border-main-black' : 'text-black'
                        }`}
                        onClick={() => handleButtonClick('Statistics')}
                    >
                        Statistics
                    </button>
                    <button
                        className={`border-b-[2px] border-gray-10 px-32  py-2 ${
                            activeButton === 'Terms of cooperation' ? 'border-main-black' : 'text-black'
                        }`}
                        onClick={() => handleButtonClick('Terms of cooperation')}
                    >
                        Terms of cooperation
                    </button>
                </div>
            </div>
            <StatisticsBar user={userByID} posts={posts} advertisements={advertisementByID}/>
            <div className="h-auto w-full py-4 px-12">
                <div className="flex justify-center">
                    <div className="h-auto w-full flex flex-row p-4 rounded-lg">
                        {hasPreviousPage && (
                            <button onClick={() => fetchPostsByID(userId!, 6, "before", beforeCursor)}>
                                <img src={left} className="w-12 h-12" alt="left navigation arrow"/>
                            </button>
                        )}
                        {loading || !posts ? (
                            <div className="grid grid-cols-3 sm:grid-cols-6 gap-16 px-2">
                                {[...Array(6)].map((_, index) => (
                                    <div key={index} className="w-full">
                                        <div className="relative">
                                            <div className="bg-gray-300 w-36 h-44 rounded-lg"></div>
                                            <div className="flex flex-col justify-center items-left gap-1 pt-2">
                                                <div className="flex items-center">
                                                    <div className="h-4 bg-gray-300 rounded w-full"></div>
                                                </div>
                                                <div className="flex items-center">
                                                    <div className="h-4 bg-gray-300 rounded w-3/4"></div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                ))}
                            </div>
                        ) : (
                            <div className="grid grid-cols-3 sm:grid-cols-6 gap-16 px-2">
                                {posts.map(post => (
                                    <div key={post.id} className="flex-shrink-0 w-full">
                                        <div className="relative">
                                            {post.mediaType === 'IMAGE' || post.mediaType === 'CAROUSEL_ALBUM' ? (
                                                <img src={post.mediaUrl} alt="Post media"
                                                     className="max-w-36 max-h-44 min-w-36 min-h-44 object-cover"/>
                                            ) : (
                                                <img src={post.thumbnailUrl ?? post.mediaUrl}
                                                     alt="Post thumbnail"
                                                     className="max-w-36 max-h-44 min-w-36 min-h-44 object-cover"/>
                                            )}
                                            <div className="flex flex-col justify-center items-left gap-1 pt-2">
                                                {post.insights.find(insight => insight.name === 'likes')?.value && (
                                                    <div className="flex items-center">
                                                        <img src={like} className="w-4 h-4" alt="likes icon"/>
                                                        <p className="font-inter font-regular text-text text-sm">{post.insights.find(insight => insight.name === 'likes')?.value}</p>
                                                    </div>
                                                )}
                                                {post.insights.find(insight => insight.name === 'saved')?.value && (
                                                    <div className="flex items-center">
                                                        <img src={save} className="w-4 h-4" alt="saves icon"/>
                                                        <p className="font-inter font-regular text-text text-sm">{post.insights.find(insight => insight.name === 'saved')?.value}</p>
                                                    </div>
                                                )}
                                                {post.insights.find(insight => insight.name === 'views')?.value && (
                                                    <div className="flex items-center">
                                                        <img src={view} className="w-4 h-4" alt="views icon"/>
                                                        <p className="font-inter font-regular text-text text-sm">{post.insights.find(insight => insight.name === 'views')?.value}</p>
                                                    </div>
                                                )}
                                            </div>
                                        </div>
                                    </div>
                                ))}
                            </div>
                        )}
                        {hasNextPage && (
                            <button className="flex items-start justify-center pt-20"
                                    onClick={() => fetchPostsByID(userId!, 6, "before", beforeCursor)}>
                                <img src={right} className="w-12 h-12" alt="right navigation arrow"/>
                            </button>
                        )}
                    </div>
                </div>
            </div>
            <div className="h-1/4 w-full flex justify-center items-center">
                4
            </div>
        </div>
    );
};

export default ProfileBloggerPage;