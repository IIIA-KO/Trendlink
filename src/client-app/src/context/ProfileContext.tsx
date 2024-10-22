import {ProfileType} from "../types/ProfileType";
import {deleteProfilePhoto, updateProfile} from "../services/profile";
import {createContext, ReactNode} from "react";
import {ProfileContextType} from "../types/ProfileContextType";
import {handleError} from "../utils/handleError";

const ProfileContext = createContext<ProfileContextType | undefined>(undefined);

const ProfileProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    const deletePhoto = async () => {
        try {
            await deleteProfilePhoto();
        } catch (error) {
            handleError(error);
        }
    };

    const updateProfileData = async (profileData: ProfileType) => {
        try {
            await updateProfile(profileData);
        } catch (error) {
            handleError(error);
        }
    };

    return (
        <ProfileContext.Provider
            value={{
                deletePhoto,
                updateProfileData,
            }}
        >
            {children}
        </ProfileContext.Provider>
    );
};

export { ProfileProvider, ProfileContext };