import {AudienceGenderData} from "./AudienceGenderDataType";
import {AudienceAgeData} from "./AudienceAgeDataType";
import {AudienceLocationData} from "./AudienceLocationDataType";
import {AudienceReachData} from "./AudienceReachDataType";
import {LocationType} from "./LocationType";
import {StatisticsPeriodType} from "./StatisticsPeriodType";

export interface AudienceContextType {
    genderData: AudienceGenderData | null;
    ageData: AudienceAgeData[] | null;
    locationData: AudienceLocationData[] | null;
    reachData: AudienceReachData[] | null;
    fetchAudienceGenderPercentage: () => Promise<void>;
    fetchAudienceAgePercentage: () => Promise<void>;
    fetchAudienceLocationPercentage: (locationType: LocationType) => Promise<void>;
    fetchAudienceReachPercentage: (statisticsPeriod: StatisticsPeriodType) => Promise<void>;
}