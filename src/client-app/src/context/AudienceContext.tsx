import React, {createContext, ReactNode, useEffect, useState} from "react";
import {handleError} from "../utils/handleError";
import {getAudienceGenderPercentage} from "../services/audiences";
import {AudienceGenderData} from "../types/AudienceGenderDataType";
import {AudienceContextType} from "../types/AudienceContextType";

const AudienceContext = createContext<AudienceContextType | undefined>(undefined);

const AudienceProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    const [genderData, setGenderData] = useState<AudienceGenderData | null>(null);

    const fetchAudienceGenderPercentage = async () => {
        try {
            const genderDataResponse = await getAudienceGenderPercentage();
            setGenderData(genderDataResponse);
        } catch (error) {
            handleError(error);
        }
    };

    useEffect(() => {
        fetchAudienceGenderPercentage();
    }, []);

    return (
        <AudienceContext.Provider
            value={{
                genderData,
                fetchAudienceGenderPercentage,
            }}>
            {children}
        </AudienceContext.Provider>
    );
};

export { AudienceProvider, AudienceContext };