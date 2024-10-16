import Navbar from "../components/Navbar";
import TopBar from "../components/TopBar";
import {useProfile} from "../hooks/useProfile";
import {useNavigate} from "react-router-dom";
import StatisticsBar from "../components/StatisticsBar";

const ProfilePage: React.FC = () => {

    const { posts, loading } = useProfile();
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
                <div
                    className="flex flex-col gap-2 border border-orange bg-custom-bg bg-cover bg-no-repeat rounded-[50px] h-auto w-auto min-h-screen min-w-screen sm:mr-24 md:mr-32 lg:mr-42 xl:mr-64 mt-10">
                    <TopBar/>
                    <StatisticsBar/>
                    <div className="h-auto w-full p-6">
                        <div className="flex justify-center">
                            <div className="w-full sm:w-3/4 lg:w-2/3 border-2 border-gray-300 p-4 rounded-lg">
                                {loading || !posts ? (
                                    // Skeleton loaders
                                    <div className="grid grid-cols-2 sm:grid-cols-3 gap-4">
                                        {[...Array(6)].map((_, index) => (
                                            <div key={index} className="w-full animate-pulse">
                                                <div className="bg-gray-300 h-64 w-full rounded-lg"></div>
                                                <div className="mt-2 space-y-2">
                                                    <div className="h-4 bg-gray-300 rounded w-3/4"></div>
                                                    <div className="h-4 bg-gray-300 rounded w-1/2"></div>
                                                </div>
                                            </div>
                                        ))}
                                    </div>
                                ) : (
                                    <div className="grid grid-cols-2 sm:grid-cols-3 gap-4">
                                        {posts.map(post => (
                                            <div key={post.id} className="flex-shrink-0 w-full">
                                                <div className="relative">
                                                    <img
                                                        src={post.mediaUrl}
                                                        alt="Post media"
                                                        className="w-full h-64 object-cover rounded-lg"
                                                    />
                                                    <div
                                                        className="flex justify-between absolute bottom-2 left-2 right-2 text-white">
                                                        <div className="flex items-center">
                                                            <svg
                                                                className="w-5 h-5 mr-1"
                                                                fill="none"
                                                                stroke="currentColor"
                                                                viewBox="0 0 24 24"
                                                                xmlns="http://www.w3.org/2000/svg"
                                                            >
                                                                <path
                                                                    strokeLinecap="round"
                                                                    strokeLinejoin="round"
                                                                    strokeWidth="2"
                                                                    d="M5 13l4 4L19 7"
                                                                />
                                                            </svg>
                                                            <span>{post.insights.find(insight => insight.name === 'likes')?.value}</span>
                                                        </div>
                                                        <div className="flex items-center">
                                                            <svg
                                                                className="w-5 h-5 mr-1"
                                                                fill="none"
                                                                stroke="currentColor"
                                                                viewBox="0 0 24 24"
                                                                xmlns="http://www.w3.org/2000/svg"
                                                            >
                                                                <path
                                                                    strokeLinecap="round"
                                                                    strokeLinejoin="round"
                                                                    strokeWidth="2"
                                                                    d="M12 8v4l3 3"
                                                                />
                                                            </svg>
                                                            <span>{post.insights.find(insight => insight.name === 'saved')?.value}</span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div className="mt-2 text-center">
                                                    <h4 className="text-sm text-gray-500">{post.timestamp}</h4>
                                                    <a
                                                        href={post.permalink}
                                                        target="_blank"
                                                        rel="noopener noreferrer"
                                                        className="text-blue-500 text-sm"
                                                    >
                                                        View on Instagram
                                                    </a>
                                                </div>
                                            </div>
                                        ))}
                                    </div>
                                )}
                            </div>
                        </div>
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