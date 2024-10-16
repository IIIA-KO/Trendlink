import Navbar from "../components/Navbar";
import TopBar from "../components/TopBar";
import {useProfile} from "../hooks/useProfile";
import {useNavigate} from "react-router-dom";
import StatisticsBar from "../components/StatisticsBar";

const ProfilePage: React.FC = () => {

    const { user, advertisements, loading } = useProfile();
    const navigate = useNavigate();

    if (loading) {
        navigate("/loading")
    }

    return (
        <div className="bg-background flex justify-start h-auto w-auto">
            <div className="h-auto w-1/6 flex justify-start items-center pl-1 sm:pl-4 md:pl-6 lg:pl-10 xl:pl-22 2xl:pl-28">
                <Navbar />
            </div>
            <div className="w-5/6 h-auto">
                <div className="flex flex-col gap-2 border border-orange bg-custom-bg bg-cover bg-no-repeat rounded-[50px] h-auto w-auto min-h-screen min-w-screen sm:mr-24 md:mr-32 lg:mr-42 xl:mr-64 mt-10">
                    <TopBar />
                    <StatisticsBar />
                    <div className="h-1/4 w-full border-2 border-red-600 text-center text-black">
                        3
                    </div>
                    <div className="h-1/4 w-full border-2 border-red-600 text-center text-black">
                        4
                    </div>
                </div>
            </div>

        </div>
    );
};

export default ProfilePage;