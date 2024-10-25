import React, {useEffect, useState} from 'react';
import { useUser } from '../hooks/useUser';
import {CooperationDetailsPropsType} from "../types/CooperationDetailsPropsType";

const getCooperationStatusText = (status: number): string => {
    switch (status) {
        case 1: return "Pending";
        case 2: return "Confirmed";
        case 3: return "Rejected";
        case 4: return "Cancelled";
        case 5: return "Done";
        case 6: return "Completed";
        default: return "Unknown";
    }
};

const CooperationDetails: React.FC<CooperationDetailsPropsType> = ({ cooperation, getAdvertisementNameById }) => {
    const { fetchUserByIDRE } = useUser();
    const [buyerName, setBuyerName] = useState<string | null>(null);
    const [sellerName, setSellerName] = useState<string | null>(null);

    useEffect(() => {
        const fetchBuyer = async () => {
            if (cooperation.buyerId) {
                try {
                    const buyer = await fetchUserByIDRE(cooperation.buyerId);
                    if (buyer) {
                        setBuyerName(`${buyer.firstName} ${buyer.lastName}`);
                    }
                } catch (error) {
                    console.error("Error fetching buyer:", error);
                }
            }
        };

        const fetchSeller = async () => {
            if (cooperation.sellerId) {
                try {
                    const seller = await fetchUserByIDRE(cooperation.sellerId);
                    if (seller) {
                        setSellerName(`${seller.firstName} ${seller.lastName}`);
                    }
                } catch (error) {
                    console.error("Error fetching seller:", error);
                }
            }
        };

        fetchBuyer();
        fetchSeller();
    }, [cooperation.buyerId, cooperation.sellerId, fetchUserByIDRE]);

    const cooperationDate = new Date(cooperation.scheduledOnUtc);
    const formattedDate = cooperationDate.toLocaleDateString('uk-UA');
    const formattedTime = cooperationDate.toLocaleTimeString('uk-UA', { hour: '2-digit', minute: '2-digit' });


    return (
        <div key={cooperation.id} className="cooperation-details mb-4 p-4 bg-[#eff7ff] rounded-[20px] shadow p-6 font-['Inter'] rounded-lg shadow-md">
            <h4 className="text-lg font-normal font-['Inter'] mb-2">Cooperation</h4>
            <p className="text-sm mb-2 font-normal font-['Inter']">Buyer: {buyerName || 'Завантаження...'}</p>
            <p className="text-sm font-normal font-['Inter'] mb-2">Seller: {sellerName || 'Завантаження...'}</p>
            <p className="text-sm font-normal font-['Inter']">Date: {formattedDate}</p>
            <p className="text-sm font-normal font-['Inter']">Time: {formattedTime}</p>
            <p className="text-sm font-normal font-['Inter'] mb-2">Amount : {cooperation.priceAmount} {cooperation.priceCurrency}</p>
            <p className="text-sm font-normal font-['Inter'] mb-2">Advertisements: {getAdvertisementNameById(cooperation.advertisementId)}</p>
            <p className="text-sm font-normal font-['Inter'] mb-2">Description: {cooperation.description}</p>
            <p className="text-sm font-normal font-['Inter'] mb-2">Status: {getCooperationStatusText(cooperation.status)}</p>
        </div>
    );
};

export default CooperationDetails;


