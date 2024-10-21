import {ReactNode} from "react";
import {AdvertisementsProvider} from "../context/AdvertisementsContext";
import {UserProvider} from "../context/UserContext";
import {PostsProvider} from "../context/PostsContext";
import {AudienceProvider} from "../context/AudienceContext";
import {CalendarProvider} from "../context/CalendarContext";
import {CooperationProvider} from "../context/CooperationContext";
import {NotificationProvider} from "../context/NotificationContext";

export const DataProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    return (
        <UserProvider>
            <AdvertisementsProvider>
                <PostsProvider>
                    <AudienceProvider>
                        <CalendarProvider>
                            <CooperationProvider>
                                <NotificationProvider>
                                    {children}
                                </NotificationProvider>
                            </CooperationProvider>
                        </CalendarProvider>
                    </AudienceProvider>
                </PostsProvider>
            </AdvertisementsProvider>
        </UserProvider>
    );
};