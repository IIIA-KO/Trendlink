import instGreyIcon from "../assets/icons/instagram-grey-icon.svg";
import React, { useMemo } from "react";
import PieGraph from "./PieGraph";
import { StatisticsBarType } from "../types/StatisticsBarType";

const Engagement: React.FC<StatisticsBarType> = ({ user, posts, advertisements }) => {
    const advertisement = advertisements?.[0];

    const userStats = useMemo(() => {
        if (!user || !posts || posts.length === 0) return null;

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
        <div className="h-1/4 w-full text-center text-black">
            <div className="h-1/4 w-full flex flex-row xl:gap-20 2xl:gap-32 text-center text-black">
                <div className="h-full relative w-3/5 mx-10 my-6">
                    <div className="h-2/3 rounded-[15px] bg-background">
                        <div className="w-full h-full left-0 top-0 bg-[#f0f4f9] rounded-[20px]" />
                        <div className="w-[500px] h-[177px] left-[55px] top-[28px]">
                            <div className="w-[25px] h-[120px] left-[462px] top-[41px] absolute">
                                {Array.from({ length: 6 }, (_, i) => (
                                    <div key={i} className={`left-0 top-[${105 - i * 35}px] absolute text-[#3c3c3c] text-xs font-normal font-['Inter']`}>
                                        {i * 20}%
                                    </div>
                                ))}
                            </div>
                            <div className="left-5 top-0 absolute text-[#3c3c3c] text-base font-bold font-['Inter'] uppercase">Вік</div>
                            {/* Bars for age groups */}
                            {["18-24р", "25-34р", "35-44р", "45-54р", "55-64р"].map((ageGroup, index) => (
                                <div key={index} className={`w-96 h-5 left-0 top-[${49 + index * 35}px] absolute`}>
                                    <div className="left-0 top-[3px] absolute text-[#3c3c3c] text-sm font-normal font-['Inter']">{ageGroup}</div>
                                    <div className="w-80 h-5 left-[72px] top-0 absolute bg-[#eaeaea]" />
                                    <div className={`w-[${(Math.random() * 100).toFixed(0)}px] h-5 left-[72px] top-0 absolute bg-[#f3ae5f]`} />
                                </div>
                            ))}
                        </div>
                    </div>
                    <div className="h-2/3 rounded-[15px] mt-[10px] bg-background">
                        <div className="w-96 h-60 relative">
                            <div className="w-96 h-60 top-0 absolute bg-[#f0f4f9] rounded-2xl" />
                            <div className="w-96 h-44 left-[15px] top-[33px] absolute">
                                <div className="left-0 top-[1px] absolute text-[#3c3c3c] text-base font-bold font-['Inter'] uppercase">Топ місцезнаходження</div>
                                <div className="w-32 h-6 left-[371px] top-0 absolute">
                                    <div className="h-5 left-0 top-0 absolute flex-col justify-start items-start gap -0.5 inline-flex">
                                        <div className="self-stretch text-[#3c3c3c] text-base font-normal font-['Inter']">Міста</div>
                                    </div>
                                    <div className="h-5 left-[55px] top-[2px] absolute flex-col justify-start items-start gap-0.5 inline-flex">
                                        <div className="text-[#3c3c3c] text-base font-normal font-['Inter']">Країни</div>
                                    </div>
                                </div>
                                {/* Top locations */}
                                {["Київ", "Луцьк", "Чернігів", "Чернівці"].map((location, index) => (
                                    <div key={index} className="w-96 h-5 left-0 top-[${50 + index * 35}px] absolute">
                                        <div className="left-0 top-[3px] absolute text-[#3c3c3c] text-sm font-normal font-['Inter']">{location}</div>
                                        <div className="w-80 h-5 left-[72px] top-0 absolute bg-[#eaeaea]" />
                                        <div className={`w-[${(Math.random() * 100).toFixed(0)}px] h-5 left-[72px] top-0 absolute bg-[#f3ae5f]`} />
                                        <div className="left-[442px] top-[2px] absolute text-[#3c3c3c] text-base font-normal font-['Inter']">{(Math.random() * 100).toFixed(0)}%</div>
                                    </div>
                                ))}
                            </div>
                        </div>
                    </div>
                </div>
                <div className="h-7/8 w-1/2 right-[60px] relative mx-10 my-6 rounded-[40px] bg-background">
                    <div className="items-center mt-20">
                        <div className="w-52 h-72 flex-col justify-start items-start gap-10 inline-flex">
                            {/* Engagement rates */}
                            {Array.from({ length: 3 }, (_, i) => (
                                <div key={i} className="self-stretch h-10 flex-col justify-start items-start gap-1 flex">
                                    <div className="self-stretch text-[#3c3c3c] text-base font-bold font-['Inter']">{(Math.random() * 1).toFixed(2)}%</div>
                                    <div className="self-stretch text-[#3c3c3c] text-sm font-normal font-['Inter']">Середній коефіцієнт залучення</div>
                                </div>
                            ))}
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default Engagement;