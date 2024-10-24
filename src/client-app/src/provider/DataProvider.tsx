import {ReactNode} from "react";
import {AdvertisementsProvider} from "../context/AdvertisementsContext";
import {UserProvider} from "../context/UserContext";
import {PostsProvider} from "../context/PostsContext";
import {AudienceProvider} from "../context/AudienceContext";
import {CalendarProvider} from "../context/CalendarContext";
import {CooperationProvider} from "../context/CooperationContext";
import {NotificationProvider} from "../context/NotificationContext";
import {ProfileProvider} from "../context/ProfileContext";
import {StatisticsProvider} from "../context/StatisticsContext";
import {ReviewProvider} from "../context/ReviewContext";

export const DataProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    return (
        <UserProvider>
            <ProfileProvider>
                <AdvertisementsProvider>
                    <PostsProvider>
                        <AudienceProvider>
                            <StatisticsProvider>
                                <CalendarProvider>
                                    <CooperationProvider>
                                        <NotificationProvider>
                                            <ReviewProvider>
                                                {children}
                                            </ReviewProvider>
                                        </NotificationProvider>
                                    </CooperationProvider>
                                </CalendarProvider>
                            </StatisticsProvider>
                        </AudienceProvider>
                    </PostsProvider>
                </AdvertisementsProvider>
            </ProfileProvider>
        </UserProvider>
    );
};