import {StatisticsPeriodType} from "../types/StatisticsPeriodType";
import {MetricsTableType} from "../types/MetricsTableType";
import axiosInstance from "./api";
import {handleError} from "../utils/handleError";
import {OverviewMetricsType} from "../types/OverviewMetricsType";
import {InteractionMetricsType} from "../types/InteractionMetricsType";
import {EngagementMetricsType} from "../types/EngagementMetricsType";

export const getStatisticsTable = async (period: StatisticsPeriodType): Promise<MetricsTableType | null> => {
    try {
        const response = await axiosInstance.get(`/statistics/table`, { params: { period } });
        return response.data;
    } catch (error) {
        handleError(error);
        return null;
    }
};

export const getStatisticsTableByUserId = async (userId: string, period: StatisticsPeriodType): Promise<MetricsTableType | null> => {
    try {
        const response = await axiosInstance.get(`/statistics/table/${userId}`, { params: { period } });
        return response.data;
    } catch (error) {
        handleError(error);
        return null;
    }
};

export const getStatisticsOverview = async (period: StatisticsPeriodType): Promise<OverviewMetricsType | null> => {
    try {
        const response = await axiosInstance.get(`/statistics/overview`, { params: { period } });
        return response.data;
    } catch (error) {
        handleError(error);
        return null;
    }
};

export const getStatisticsOverviewByUserId = async (userId: string, period: StatisticsPeriodType): Promise<OverviewMetricsType | null> => {
    try {
        const response = await axiosInstance.get(`/statistics/overview/${userId}`, { params: { period } });
        return response.data;
    } catch (error) {
        handleError(error);
        return null;
    }
};

export const getStatisticsInteraction = async (period: StatisticsPeriodType): Promise<InteractionMetricsType | null> => {
    try {
        const response = await axiosInstance.get(`/statistics/interaction`, { params: { period } });
        return response.data;
    } catch (error) {
        handleError(error);
        return null;
    }
};

export const getStatisticsInteractionByUserId = async (userId: string, period: StatisticsPeriodType): Promise<InteractionMetricsType | null> => {
    try {
        const response = await axiosInstance.get(`/statistics/interaction/${userId}`, { params: { period } });
        return response.data;
    } catch (error) {
        handleError(error);
        return null;
    }
};

export const getStatisticsEngagement = async (period: StatisticsPeriodType): Promise<EngagementMetricsType | null> => {
    try {
        const response = await axiosInstance.get(`/statistics/engagement`, { params: { period } });
        return response.data;
    } catch (error) {
        handleError(error);
        return null;
    }
};

export const getStatisticsEngagementByUserId = async (userId: string, period: StatisticsPeriodType): Promise<EngagementMetricsType | null> => {
    try {
        const response = await axiosInstance.get(`/statistics/engagement/${userId}`, { params: { period } });
        return response.data;
    } catch (error) {
        handleError(error);
        return null;
    }
};