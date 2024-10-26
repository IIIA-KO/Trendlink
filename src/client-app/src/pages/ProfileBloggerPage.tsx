import TopBar from "../components/TopBar";
import {useParams} from "react-router-dom";
import React, {useEffect, useState} from "react";
import {usePosts} from "../hooks/usePosts";
import {useAdvertisements} from "../hooks/useAdvertisements";
import {useUser} from "../hooks/useUser";
import BloggerViews from "../components/BloggerViews";
import BloggerTermsOfCooperation from "../components/BloggerTermsOfCooperation";

const ProfileBloggerPage: React.FC = () => {
    const { userId } = useParams<{ userId: string }>();
    const [activeButton, setActiveButton] = useState<string>('Views');

    const { userByID, fetchUserByID } = useUser();
    const { advertisementByID, fetchAdvertisementsByID } = useAdvertisements();
    const {
        posts,
        fetchPostsByID,
    } = usePosts()


    useEffect(() => {
        if (userId) {
            fetchUserByID(userId);
            fetchAdvertisementsByID(userId);
            fetchPostsByID(userId);
        }
    }, [userId]);


    const handleButtonClick = (buttonName: string) => {
        setActiveButton(buttonName);
    };

    return (
        <div
            className="flex flex-col justify-center items-center gap-2 bg-custom-bg bg-cover bg-no-repeat rounded-[50px] h-auto w-auto min-h-screen min-w-screen sm:mr-24 md:mr-32 lg:mr-42 xl:mr-64 mt-10">
            <TopBar user={userByID} showButton={"off"}/>
            <div className="w-[92%] h-full flex justify-center items-center">
                <div className="flex w-full space-x-2">
                    <button
                        className={`flex-1 border-b-[2px] border-gray-10 px-32 ${
                            activeButton === 'Views' ? 'border-main-black' : 'text-black'
                        }`}
                        onClick={() => handleButtonClick('Views')}
                    >
                        Views
                    </button>
                    <button
                        className={`flex-1 border-b-[2px] border-gray-10 px-32  py-2 ${
                            activeButton === 'Terms of cooperation' ? 'border-main-black' : 'text-black'
                        }`}
                        onClick={() => handleButtonClick('Terms of cooperation')}
                    >
                        Terms of cooperation
                    </button>
                </div>
            </div>
            <div className="w-full mt-8">
                {activeButton === 'Views' && (
                    <div>
                        <BloggerViews userID={userId} user={userByID} posts={posts} advertisements={advertisementByID}/>
                    </div>
                )}
                {activeButton === 'Statistics' && (
                    <div>
                        <p>Контент для Statistics</p>
                    </div>
                )}
                {activeButton === 'Terms of cooperation' && (
                    <div>
                        <BloggerTermsOfCooperation userID={userId} user={userByID} posts={posts} advertisements={advertisementByID} />
                    </div>
                )}
            </div>

        </div>
    );
};

export default ProfileBloggerPage;