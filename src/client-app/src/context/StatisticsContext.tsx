import React, { createContext, ReactNode, useState } from "react";
import {
    getStatisticsTable,
    getStatisticsTableByUserId,
    getStatisticsOverview,
    getStatisticsOverviewByUserId,
    getStatisticsInteraction,
    getStatisticsInteractionByUserId,
    getStatisticsEngagement,
    getStatisticsEngagementByUserId
} from "../services/statistics";
import { MetricsTableType } from "../types/MetricsTableType";
import { OverviewMetricsType } from "../types/OverviewMetricsType";
import { InteractionMetricsType } from "../types/InteractionMetricsType";
import { EngagementMetricsType } from "../types/EngagementMetricsType";
import { StatisticsPeriodType } from "../types/StatisticsPeriodType";
import { handleError } from "../utils/handleError";
import {StatisticsContextType} from "../types/StatisticsContextType";

const StatisticsContext = createContext<StatisticsContextType | undefined>(undefined);

const StatisticsProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    const [tableData, setTableData] = useState<MetricsTableType | null>(null);
    const [overviewData, setOverviewData] = useState<OverviewMetricsType | null>(null);
    const [interactionData, setInteractionData] = useState<InteractionMetricsType | null>(null);
    const [engagementData, setEngagementData] = useState<EngagementMetricsType | null>(null);

    const fetchStatisticsTable = async (period: StatisticsPeriodType) => {
        try {
            const data = await getStatisticsTable(period);
            setTableData(data);
        } catch (error) {
            handleError(error);
        }
    };

    const fetchStatisticsOverview = async (period: StatisticsPeriodType) => {
        try {
            const data = await getStatisticsOverview(period);
            setOverviewData(data);
        } catch (error) {
            handleError(error);
        }
    };

    const fetchStatisticsInteraction = async (period: StatisticsPeriodType) => {
        try {
            const data = await getStatisticsInteraction(period);
            setInteractionData(data);
        } catch (error) {
            handleError(error);
        }
    };

    const fetchStatisticsEngagement = async (period: StatisticsPeriodType) => {
        try {
            const data = await getStatisticsEngagement(period);
            setEngagementData(data);
        } catch (error) {
            handleError(error);
        }
    };

    const fetchStatisticsTableById = async (userId: string, period: StatisticsPeriodType) => {
        try {
            const data = await getStatisticsTableByUserId(userId, period);
            setTableData(data);
        } catch (error) {
            handleError(error);
        }
    };

    const fetchStatisticsOverviewById = async (userId: string, period: StatisticsPeriodType) => {
        try {
            const data = await getStatisticsOverviewByUserId(userId, period);
            setOverviewData(data);
        } catch (error) {
            handleError(error);
        }
    };

    const fetchStatisticsInteractionById = async (userId: string, period: StatisticsPeriodType) => {
        try {
            const data = await getStatisticsInteractionByUserId(userId, period);
            setInteractionData(data);
        } catch (error) {
            handleError(error);
        }
    };

    const fetchStatisticsEngagementById = async (userId: string, period: StatisticsPeriodType) => {
        try {
            const data = await getStatisticsEngagementByUserId(userId, period);
            setEngagementData(data);
        } catch (error) {
            handleError(error);
        }
    };

    return (
        <StatisticsContext.Provider
            value={{
                tableData,
                overviewData,
                interactionData,
                engagementData,
                fetchStatisticsTable,
                fetchStatisticsTableById,
                fetchStatisticsOverview,
                fetchStatisticsOverviewById,
                fetchStatisticsInteraction,
                fetchStatisticsInteractionById,
                fetchStatisticsEngagement,
                fetchStatisticsEngagementById,
            }}
        >
            {children}
        </StatisticsContext.Provider>
    );
};

export { StatisticsProvider, StatisticsContext };