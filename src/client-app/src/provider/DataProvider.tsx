import {ReactNode} from "react";
import {AdvertisementsProvider} from "../context/AdvertisementsContext";
import {UserProvider} from "../context/UserContext";
import {PostsProvider} from "../context/PostsContext";
import {AudienceProvider} from "../context/AudienceContext";

export const DataProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    return (
        <UserProvider>
            <AdvertisementsProvider>
                <PostsProvider>
                    <AudienceProvider>
                        {children}
                    </AudienceProvider>
                </PostsProvider>
            </AdvertisementsProvider>
        </UserProvider>
    );
};