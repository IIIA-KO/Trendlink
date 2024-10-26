import StatisticsBar from "./StatisticsBar";
import React, {useEffect, useState} from "react";
import imgTerms from "../assets/BloggerTermsOfCoopimg.svg"
import {BloggerComponentType} from "../types/BloggerComponentType";
import {getTermsAndConditionsByID} from "../services/termsofcooperation";
import {TermsAndConditionsType} from "../types/TermsAndConditionsType";
import RequestModal from "./RequestModal";

const BloggerTermsOfCooperation: React.FC<BloggerComponentType> = ({ userID, user, posts, advertisements }) => {

    const [termsData, setTermsData] = useState<Omit<TermsAndConditionsType, 'userId'> | null>(null);
    const [isModalOpen, setIsModalOpen] = useState(false);

    useEffect(() => {
        const fetchTerms = async () => {
            if (!userID) return;
            try {
                const data = await getTermsAndConditionsByID(userID);
                if (data) {
                    setTermsData(data);
                } else {
                    setTermsData({
                        id: '',
                        description: '',
                        advertisements: [
                            {
                                id: '',
                                name: '',
                                priceAmount: 0,
                                priceCurrency: '',
                                description: ''
                            }
                        ]
                    });
                }
            } catch (error) {
                console.error("Error fetching terms and conditions:", error);
            }
        };

        fetchTerms();
    }, [userID]);

    if (!termsData) {
        return <div>Loading...</div>;
    }

    const handleOrderClick = () => {
        setIsModalOpen(true);
    };

    const closeModal = () => {
        setIsModalOpen(false);
    };

    return (
        <div>
            <StatisticsBar user={user} posts={posts} advertisements={advertisements}/>
            <div className="h-full w-full flex flex-row px-6">
                <div className="h-full w-1/2 flex flex-col justify-center items-center">
                    <div className="h-1/2 w-full p-6">
                        <p className="font-inter font-regular text-[16px]">{termsData.description}</p>
                    </div>
                    <div className="h-1/2 w-full p-6">
                        <div className="grid grid-cols-2 font-bold text-left pb-1 pl-4 font-inter inter-light-italic text-[12px] text-text-2">
                            <p>Types of services</p>
                            <p>Cost</p>
                        </div>
                        <ul className="space-y-4">
                            {termsData.advertisements.map((ad, index) => (
                                <li key={index}
                                    className="grid grid-cols-2 items-center gap-4">
                                    <input
                                        type="text"
                                        readOnly
                                        value={ad.name}
                                        className="focus:outline-none text-left font-inter font-light text-[14px] border border-gray-10 rounded-[10px] p-2 whitespace-nowrap overflow-hidden text-ellipsis cursor-text w-full"
                                        title={ad.name}
                                    />
                                    <p className="w-1/3 text-left font-inter font-light text-[14px] border border-gray-10 rounded-[10px] p-2">
                                        {ad.priceAmount} {ad.priceCurrency}
                                    </p>
                                </li>
                            ))}
                        </ul>
                        <div className="mt-12 mb-16 w-1/2">
                            <button onClick={handleOrderClick} className="py-2 px-8 w-full border-2 border-primary bg-primary text-center text-textPrimary text-[1rem] rounded-full transition duration-500 ease-in-out hover:bg-hover hover:border-hover hover:scale-105 active:scale-90 active:bg-transparent active:border-primary active:text-textSecondary focus:scale-100 transform">
                                Замовити рекламу
                            </button>
                        </div>
                    </div>
                </div>
                <div className="h-full w-1/2 flex items-center justify-center">
                    <img alt="img" className="h-1/2 w-auto object-cover" src={imgTerms}/>
                </div>
            </div>

            <RequestModal
                advertisements={termsData.advertisements}
                isOpen={isModalOpen}
                onClose={closeModal}
            />

        </div>
    );
};

export default BloggerTermsOfCooperation;