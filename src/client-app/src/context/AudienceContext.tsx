import React, {createContext, ReactNode, useEffect, useState} from "react";
import {handleError} from "../utils/handleError";
import {
    getAudienceAgePercentage,
    getAudienceGenderPercentage,
    getAudienceLocationPercentage, getAudienceReachPercentage
} from "../services/audiences";
import {AudienceGenderData} from "../types/AudienceGenderDataType";
import {AudienceContextType} from "../types/AudienceContextType";
import {AudienceAgeData} from "../types/AudienceAgeDataType";
import {AudienceLocationData} from "../types/AudienceLocationDataType";
import {AudienceReachData} from "../types/AudienceReachDataType";
import {LocationType} from "../types/LocationType";
import {StatisticsPeriod} from "../types/StatisticsPeriodType";

const AudienceContext = createContext<AudienceContextType | undefined>(undefined);

const AudienceProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    const [genderData, setGenderData] = useState<AudienceGenderData | null>(null);
    const [ageData, setAgeData] = useState<AudienceAgeData[] | null>(null);
    const [locationData, setLocationData] = useState<AudienceLocationData[] | null>(null);
    const [reachData, setReachData] = useState<AudienceReachData[] | null>(null);

    const fetchAudienceGenderPercentage = async () => {
        try {
            const genderDataResponse = await getAudienceGenderPercentage();
            setGenderData(genderDataResponse);
        } catch (error) {
            handleError(error);
        }
    };

    const fetchAudienceAgePercentage = async () => {
        try {
            const ageDataResponse = await getAudienceAgePercentage();
            setAgeData(ageDataResponse);
        } catch (error) {
            handleError(error);
        }
    };

    const fetchAudienceLocationPercentage = async (locationType: LocationType) => {
        try {
            const locationDataResponse = await getAudienceLocationPercentage(locationType);
            setLocationData(locationDataResponse);
        } catch (error) {
            handleError(error);
        }
    };

    const fetchAudienceReachPercentage = async (statisticsPeriod: StatisticsPeriod) => {
        try {
            const reachDataResponse = await getAudienceReachPercentage(statisticsPeriod);
            setReachData(reachDataResponse);
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
                ageData,
                locationData,
                reachData,
                fetchAudienceGenderPercentage,
                fetchAudienceAgePercentage,
                fetchAudienceLocationPercentage,
                fetchAudienceReachPercentage,
            }}
        >
            {children}
        </AudienceContext.Provider>
    );
};

export { AudienceProvider, AudienceContext };