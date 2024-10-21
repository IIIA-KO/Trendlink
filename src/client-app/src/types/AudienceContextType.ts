import {AudienceGenderData} from "./AudienceGenderDataType";

export type AudienceContextType = {
    genderData: AudienceGenderData | null;
    fetchAudienceGenderPercentage: () => void;
};