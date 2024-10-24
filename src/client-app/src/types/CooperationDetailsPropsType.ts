import {CooperationType} from "./CooperationType";

export interface CooperationDetailsPropsType {
    cooperation: CooperationType;
    getAdvertisementNameById: (advertisementId: string) => string;
}