import React from 'react';
import { CooperationType } from "../types/CooperationType";
import { useUser } from '../hooks/useUser';

interface CooperationDetailsProps {
    cooperation: CooperationType;
    getAdvertisementNameById: (advertisementId: string) => string;
    openEditModal: (cooperation: CooperationType) => void;
}

const CooperationDetails: React.FC<CooperationDetailsProps> = ({ cooperation, getAdvertisementNameById, openEditModal }) => {
    const { user } = useUser();

    return (
        <div key={cooperation.id} className="cooperation-details mb-4">
            <h4 className="text-lg font-semibold mb-2">Зустріч з: {user?.firstName} {user?.lastName}</h4>
            <p className="text-sm">Дата: {new Date(cooperation.scheduledOnUtc).toLocaleString()}</p>
            <p className="text-sm mb-2">Сума: {cooperation.priceAmount} {cooperation.priceCurrency}</p>
            <p className="text-sm mb-2">Реклама: {getAdvertisementNameById(cooperation.advertisementId)}</p>
            <p className="text-sm mb-2">Опис: {cooperation.description}</p>
            <p className="text-sm mb-2">Статус: {cooperation.status}</p>
            <button
                className="px-4 py-2 bg-blue-500 text-white rounded-md hover:bg-blue-600"
                onClick={() => openEditModal(cooperation)}
            >
                Редагувати кооперацію
            </button>
        </div>
    );
};

export default CooperationDetails;


