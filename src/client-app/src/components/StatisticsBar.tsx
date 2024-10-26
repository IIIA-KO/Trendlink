import instGreyIcon from "../assets/icons/instagram-grey-icon.svg";
import React, {useMemo} from "react";
import PieGraph from "./PieGraph";
import {StatisticsBarType} from "../types/StatisticsBarType";

const StatisticsBar: React.FC<StatisticsBarType> = ({ user, posts, advertisements }) => {

    const advertisement = advertisements?.[0];

    const userStats = useMemo(() => {
        if (!user || !posts || posts.length === 0) return null;

        if (!posts || posts.length === 0) {
            return null;
        }

        const lastPost = posts[0];
        const likeInsight = lastPost.insights.find(insight => insight.name === 'likes');
        const lastPostLikes = likeInsight ? likeInsight.value : null;

        return {
            mediaCount: user.mediaCount || 'N/A',
            averagePriceRange: advertisement?.value || '0',
            averagePriceRangeCurrency: advertisement?.currency || '',
            followersCount: user.followersCount || 'N/A',
            likesOnLastPost: lastPostLikes || 'N/A'
        };
    }, [user, advertisements, posts]);

    const currencySymbols: { [key: string]: string } = {
        'USD': '$',
        'EUR': '€',
        'UAH': '₴'
    };

    const currencySymbol = currencySymbols[advertisement?.currency || ''];

    return (
        <div className="h-1/4 w-full flex flex-row xl:gap-20 2xl:gap-32 text-center">
            <div
                className="min-h-full w-1/2 mx-10 my-6 flex flex-row rounded-[15px] bg-background">
                <div className="h-auto w-1/3 flex justify-center items-center">
                    <p className="font-inter font-bold text-xl text-text inline-flex items-center">
                        <img src={instGreyIcon} alt="Subscribe icon" className="w-7 h-7 mr-1" />
                        {userStats?.followersCount || 'N/A'}
                    </p>
                </div>
                <div className="h-full w-2/3 flex flex-col gap-4 py-9 px-2">
                    <div className="flex flex-row">
                        <div className="w-1/2 pr-6">
                            <p className="font-inter font-bold text-[14px] text-text text-right">
                                {advertisement?.value} {currencySymbol}
                            </p>
                        </div>
                        <div className="w-1/2">
                            <p className="font-inter font-regular text-[14px] text-text text-left"> Average price range</p>
                        </div>
                    </div>
                    <div className="flex flex-row">
                        <div className="w-1/2 pr-6">
                            <p className="font-inter font-bold text-[14px] text-text text-right">{userStats?.mediaCount || 'N/A'}</p>
                        </div>
                        <div className="w-1/2">
                            <p className="font-inter font-regular text-[14px] text-text text-left"> Posts</p>
                        </div>
                    </div>
                    <div className="flex flex-row">
                        <div className="w-1/2 pr-6">
                            <p className="font-inter font-bold text-[14px] text-text text-right">{userStats?.likesOnLastPost || 'N/A'}</p>
                        </div>
                        <div className="w-1/2">
                            <p className="font-inter font-regular text-[14px] text-text text-left"> Likes </p>
                            <p className="font-inter font-light text-[12px] text-text text-left">on last post</p>
                        </div>
                    </div>
                </div>
            </div>
            <div className="min-h-full w-1/2 mx-10 my-6 flex justify-center items-center rounded-[15px] bg-background">
                <PieGraph />
            </div>
        </div>
    );

}

export default StatisticsBar;
