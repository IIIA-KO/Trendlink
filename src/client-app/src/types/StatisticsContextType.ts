import {MetricsTableType} from "./MetricsTableType";
import {OverviewMetricsType} from "./OverviewMetricsType";
import {InteractionMetricsType} from "./InteractionMetricsType";
import {EngagementMetricsType} from "./EngagementMetricsType";
import {StatisticsPeriodType} from "./StatisticsPeriodType";

export interface StatisticsContextType {
    tableData: MetricsTableType | null;
    overviewData: OverviewMetricsType | null;
    interactionData: InteractionMetricsType | null;
    engagementData: EngagementMetricsType | null;
    fetchStatisticsTable: (period: StatisticsPeriodType) => Promise<void>;
    fetchStatisticsTableById: (userId: string, period: StatisticsPeriodType) => Promise<void>;
    fetchStatisticsOverview: (period: StatisticsPeriodType) => Promise<void>;
    fetchStatisticsOverviewById: (userId: string, period: StatisticsPeriodType) => Promise<void>;
    fetchStatisticsInteraction: (period: StatisticsPeriodType) => Promise<void>;
    fetchStatisticsInteractionById: (userId: string, period: StatisticsPeriodType) => Promise<void>;
    fetchStatisticsEngagement: (period: StatisticsPeriodType) => Promise<void>;
    fetchStatisticsEngagementById: (userId: string, period: StatisticsPeriodType) => Promise<void>;
}