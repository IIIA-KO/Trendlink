import TopBar from "../components/TopBar";
import StatisticsBar from "../components/StatisticsBar";
import {useUser} from "../hooks/useUser";
import { useNavigate } from "react-router-dom";
import {usePosts} from "../hooks/usePosts";
import {useAdvertisements} from "../hooks/useAdvertisements";
import {useStatistics} from "../hooks/useStatistics";
import {useEffect} from "react";
import {useAudience} from "../hooks/useAudience";
import StraightGraph from "../components/StraightGraph";

const StatisticsPage: React.FC = () => {

  const { user } = useUser();
  const { posts, fetchPosts, hasNextPage, hasPreviousPage, afterCursor, beforeCursor, loading } = usePosts();
  const { advertisements } = useAdvertisements();
  const {fetchAudienceAgePercentage} = useAudience()
  const {tableData, overviewData, interactionData, engagementData,  fetchStatisticsTable, fetchStatisticsOverview, fetchStatisticsInteraction, fetchStatisticsEngagement} = useStatistics();

  useEffect(() => {
    fetchAudienceAgePercentage();
    fetchStatisticsTable(7);
    fetchStatisticsOverview(7);
    fetchStatisticsInteraction(7);
    fetchStatisticsEngagement(7);
  }, []);

  const navigate = useNavigate();
  if (loading) {
    navigate("/loading")
  }

  return (
      <div
          className="flex flex-col items-center gap-2 bg-custom-bg bg-cover bg-no-repeat rounded-[50px] h-auto w-auto min-h-screen min-w-screen sm:mr-24 md:mr-32 lg:mr-42 xl:mr-64 mt-10">
        <TopBar user={user}/>
        <StatisticsBar user={user} posts={posts} advertisements={advertisements}/>
        <div className="flex flex-row h-full w-[95%] border border-2">
          <div className="flex col h-full w-full border border-2">
            <div className="min-h-full w-1/2 mx-10 my-6 flex flex-row rounded-[15px] bg-background">
              <StraightGraph/>
            </div>
            <div className="min-h-full w-1/2 mx-10 my-6 flex flex-row rounded-[15px] bg-background">
              <StraightGraph/>
            </div>
          </div>
          <div className="flex col h-full w-full border border-2">

          </div>
        </div>

      </div>
  );
};

export default StatisticsPage;