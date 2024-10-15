import React, {useMemo} from "react";
import Navbar from "../components/Navbar";
import TopBar from "../components/TopBar";
import instGreyIcon from "../assets/icons/instagram-grey-icon.svg"
import {useProfile} from "../hooks/useProfile";
import {useNavigate} from "react-router-dom";

const ProfilePage: React.FC = () => {

    const { user, advertisements, loading } = useProfile();
    const navigate = useNavigate();

    if (loading) {
        navigate("/loading")
    }

    const userStats = useMemo(() => {
        if (!user) return null;

        return {
            mediaCount: user.mediaCount || 'N/A',
            averagePriceRange: advertisements?.value || 'N/A',
            averagePriceRangeCurrency: advertisements?.currency || '',
            followersCount: user.followersCount || 'N/A',
            //likesOnLastPost: user.likesOnLastPost || 'N/A'
        };
    }, [user, advertisements]);

    return (
        <div className="bg-background flex justify-start h-auto w-auto">
            <div className="h-auto w-1/6 flex justify-start items-center pl-1 sm:pl-4 md:pl-6 lg:pl-10 xl:pl-22 2xl:pl-28">
                <Navbar />
            </div>
            <div className="w-5/6 h-auto">
                <div className="flex flex-col gap-2 border border-orange bg-custom-bg bg-cover bg-no-repeat rounded-[50px] h-auto w-auto min-h-screen min-w-screen sm:mr-24 md:mr-32 lg:mr-42 xl:mr-64 mt-10">
                    <TopBar />
                    <div className="h-1/4 w-full flex flex-row xl:gap-20 2xl:gap-32 border-2 border-red-600 text-center text-black">
                        <div className="h-full w-1/2 mx-10 my-6 flex flex-row rounded-[15px] bg-background border-2 border-emerald-700">
                            <div className="h-auto w-1/3 flex justify-center items-center">
                                <p className="font-inter font-bold text-xl text-text inline-flex items-center">
                                    <img src={instGreyIcon} alt="Subscribe icon" className="w-7 h-7 mr-1"/>
                                    {userStats?.followersCount || 'N/A'}
                                </p>
                            </div>
                            <div className="h-full w-2/3 flex flex-col gap-3 py-4 border-red-600 border-2">
                                <div className="flex flex-row">
                                    <div className="w-1/2 pr-6">
                                        <p className="font-inter font-bold text-[14px] text-text text-right">{userStats?.averagePriceRange || 'N/A'} {userStats?.averagePriceRangeCurrency === 'USD' ? '$' : userStats?.averagePriceRangeCurrency === 'EUR' ? '€' : userStats?.averagePriceRangeCurrency === 'UAH' ? '₴' : '?'}</p>
                                    </div>
                                    <div className="w-1/2">
                                        <p className="font-inter font-regular text-[14px] text-text text-left"> Середня
                                            ціна на рекламу</p>
                                    </div>
                                </div>
                                <div className="flex flex-row">
                                    <div  className="w-1/2 pr-6">
                                        <p className="font-inter font-bold text-[14px] text-text text-right">{userStats?.mediaCount || 'N/A'}</p>
                                    </div>
                                    <div  className="w-1/2">
                                        <p className="font-inter font-regular text-[14px] text-text text-left"> Публікацій</p>
                                    </div>
                                </div>
                                <div className="flex flex-row">
                                    <div  className="w-1/2 pr-6">
                                        <p className="font-inter font-bold text-[14px] text-text text-right">606</p>
                                    </div>
                                    <div  className="w-1/2">
                                        <p className="font-inter font-regular text-[14px] text-text text-left"> Лайків </p>
                                        <p className="font-inter font-light text-[12px] text-text text-left">на останній пост</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div
                            className="h-full w-1/2 mx-10 my-6 rounded-[40px] bg-background border-2 border-emerald-700">
                            02
                        </div>
                    </div>
                    <div className="h-1/4 w-full border-2 border-red-600 text-center text-black">
                        3
                    </div>
                    <div className="h-1/4 w-full border-2 border-red-600 text-center text-black">
                        4
                    </div>
                </div>
            </div>

        </div>
    );
};

export default ProfilePage;