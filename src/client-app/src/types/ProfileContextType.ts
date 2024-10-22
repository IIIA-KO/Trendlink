import {ProfileType} from "./ProfileType";

export interface ProfileContextType {
    deletePhoto: () => Promise<void>;
    updateProfileData: (profileData: ProfileType) => Promise<void>;
}