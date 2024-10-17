import {AudienceAgeData} from "../types/AudienceAgeDataType";
import axiosInstance from "./api";
import {handleError} from "../utils/handleError";
import {AudienceGenderData} from "../types/AudienceGenderDataType";
import {AudienceLocationData} from "../types/AudienceLocationDataType";
import {LocationType} from "../types/LocationType";
import {StatisticsPeriod} from "../types/StatisticsPeriodType";
import {AudienceReachData} from "../types/AudienceReachDataType";

export const getAudienceAgePercentage = async (): Promise<AudienceAgeData[] | null> => {
    try {
        const response = await axiosInstance.get('/audience/age');
        return response.data as AudienceAgeData[];
    } catch (error) {
        handleError(error);
        return null;
    }
};

export const getAudienceGenderPercentage = async (): Promise<AudienceGenderData | null> => {
    try {
        const response = await axiosInstance.get('/audience/gender');
        return response.data as AudienceGenderData;
    } catch (error) {
        handleError(error);
        return null;
    }
};

export const getAudienceLocationPercentage = async (locationType: LocationType): Promise<AudienceLocationData[] | null> => {
    try {
        const response = await axiosInstance.get(`/audience/location?locationType=${locationType}`);
        return response.data as AudienceLocationData[];
    } catch (error) {
        handleError(error);
        return null;
    }
};

export const getAudienceReachPercentage = async (statisticsPeriod: StatisticsPeriod): Promise<AudienceReachData[] | null> => {
    try {
        const response = await axiosInstance.get(`/audience/reach?period=${statisticsPeriod}`);
        return response.data as AudienceReachData[];
    } catch (error) {
        handleError(error);
        return null;
    }
};