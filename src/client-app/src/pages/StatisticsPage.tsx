import Navbar from "../components/Navbar";
import TopBar from "../components/TopBar";
import StatisticsBar from "../components/StatisticsBar";
import { useProfile } from "../hooks/useProfile";
import { useNavigate } from "react-router-dom";

const StatisticsPage: React.FC = () => {
  const { user, advertisements, posts, loading } = useProfile();

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
        <div className="flex flex-col gap-2 bg-custom-bg bg-cover bg-no-repeat rounded-[50px] h-auto w-auto min-h-screen min-w-screen sm:mr-24 md:mr-32 lg:mr-42 xl:mr-64 mt-10">
          <TopBar user={user} />
          <StatisticsBar user={user} posts={posts} advertisements={advertisements} />
          <div className="h-1/4 w-full text-center text-black container">
            <div className="block-current">Аудиторії</div>
            <div className="block">Публікації</div>
            <div className="block">Історії</div>
            <div className="block">Reels</div>
            <div className="block">Залучення</div>
          </div>
          <div className="h-1/4 w-full text-center text-black">
            <div className="h-1/4 w-full flex flex-row xl:gap-20 2xl:gap-32  text-center text-black">
              <div className="h-full relative w-3/5 mx-10 my-6">
                <div className="h-2/3 rounded-[15px] bg-background">
                  <div className="w-full h-full left-0 top-0 bg-[#f0f4f9] rounded-[20px]" />
                  <div className="w-[500px] h-[177px] left-[55px] top-[28px]">
                    <div className="w-[25px] h-[120px] left-[462px] top-[41px] absolute">
                      <div className="left-0 top-[105px] absolute text-[#3c3c3c] text-xs font-normal font-['Inter']">0%</div>
                      <div className="left-0 top-[70px] absolute text-[#3c3c3c] text-xs font-normal font-['Inter']">20%</div>
                      <div className="left-0 top-[35px] absolute text-[#3c3c3c] text-xs font-normal font-['Inter']">40%</div>
                      <div className="left-0 top-0 absolute text-[#3c3c3c] text-xs font-normal font-['Inter']">60%</div>
                    </div>
                    <div className="left-5 top-0 absolute text-[#3c3c3c] text-base font-bold font-['Inter'] uppercase">Вік</div>
                    <div className="w-[449px] h-[0px] left-[5px] top-[154px] absolute border border-[#c0bebe]"></div>
                    <div className="w-[449px] h-[0px] left-[5px] top-[84px] absolute border border-[#c0bebe]"></div>
                    <div className="w-[449px] h-[0px] left-[5px] top-[119px] absolute border border-[#c0bebe]"></div>
                    <div className="w-[449px] h-[0px] left-[5px] top-[49px] absolute border border-[#c0bebe]"></div>
                    <div className="h-[15px] left-[19px] top-[162px] absolute justify-start items-start gap-[52px] inline-flex">
                      <div className="text-[#3c3c3c] text-xs font-normal font-['Inter']">18-24р</div>
                      <div className="text-[#3c3c3c] text-xs font-normal font-['Inter']">25-34р</div>
                      <div className="text-[#3c3c3c] text-xs font-normal font-['Inter']">35-44р</div>
                      <div className="text-[#3c3c3c] text-xs font-normal font-['Inter']">45-54р</div>
                      <div className="text-[#3c3c3c] text-xs font-normal font-['Inter']">55-64р</div>
                    </div>
                    <div className="w-[22px] h-[9px] left-[24px] top-[145px] absolute bg-[#0b87ba]" />
                    <div className="w-[22px] h-1 left-[407px] top-[150px] absolute bg-[#0b87ba]" />
                    <div className="w-[22px] h-[35px] left-[119px] top-[119px] absolute bg-[#0b87ba]" />
                    <div className="w-[22px] h-[31px] left-[311px] top-[123px] absolute bg-[#0b87ba]" />
                    <div className="w-[22px] h-[89px] left-[214px] top-[65px] absolute bg-[#0b87ba]" />
                  </div>
                </div>
                <div className="h-2/3 rounded-[15px] mt-[10px] bg-background">
                  <div className="w-96 h-60 relative">
                    <div className="w-96 h-60 top-0 absolute bg-[#f0f4f9] rounded-2xl" />
                    <div className="w-96 h-44 left-[15px] top-[33px] absolute">
                      <div className="left-0 top-[1px] absolute text-[#3c3c3c] text-base font-bold font-['Inter'] uppercase">Топ місцезнаходження</div>
                      <div className="w-32 h-6 left-[371px] top-0 absolute">
                        <div className="h-5 left-0 top-0 absolute flex-col justify-start items-start gap-0.5 inline-flex">
                          <div className="self-stretch text-[#3c3c3c] text-base font-normal font-['Inter']">Міста</div>
                        </div>
                        <div className="h-5 left-[55px] top-[2px] absolute flex-col justify-start items-start gap-0.5 inline-flex">
                          <div className="text-[#3c3c3c] text-base font-normal font-['Inter']">Країни</div>
                        </div>
                      </div>
                      <div className="w-96 h-5 left-0 top-[50px] absolute">
                        <div className="left-0 top-0 absolute text-[#3c3c3c] text-sm font-normal font-['Inter']">Київ</div>
                        <div className="left-[442px] top-[2px] absolute text-[#3c3c3c] text-base font-normal font-['Inter']">60%</div>
                        <div className="w-80 h-5 left-[72px] top-0 absolute bg-[#eaeaea]" />
                        <div className="w-60 h-5 left-[72px] top-0 absolute bg-[#f3ae5f]" />
                      </div>
                      <div className="w-96 h-5 left-0 top-[87px] absolute">
                        <div className="left-0 top-[1px] absolute text-[#3c3c3c] text-sm font-normal font-['Inter']">Луцьк</div>
                        <div className="left-[442px] top-[2px] absolute text-[#3c3c3c] text-base font-normal font-['Inter']">30%</div>
                        <div className="w-80 h-5 left-[72px] top-0 absolute bg-[#eaeaea]" />
                        <div className="w-24 h-5 left-[72px] top-0 absolute bg-[#f3ae5f]" />
                      </div>
                      <div className="w-96 h-5 left-0 top-[122px] absolute">
                        <div className="left-0 top-[3px] absolute text-[#3c3c3c] text-sm font-normal font-['Inter']">Чернігів</div>
                        <div className="w-80 h-5 left-[72px] top-0 absolute bg-[#eaeaea]" />
                        <div className="left-[442px] top-[2px] absolute text-[#3c3c3c] text-base font-normal font-['Inter']">20%</div>
                        <div className="w-12 h-5 left-[72px] top-0 absolute bg-[#f3ae5f]" />
                      </div>
                      <div className="w-96 h-5 left-0 top-[159px] absolute">
                        <div className="left-0 top-[3px] absolute text-[#3c3c3c] text-sm font-normal font-['Inter']">Чернівці</div>
                        <div className="w-80 h-5 left-[72px] top-0 absolute bg-[#eaeaea]" />
                        <div className="left-[442px] top-[2px] absolute text-[#3c3c3c] text-base font-normal font-['Inter']">25%</div>
                        <div className="w-20 h-5 left-[72px] top-0 absolute bg-[#f3ae5f]" />
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <div className="h-7/8 w-1/2 right-[60px] relative mx-10 my-6 rounded-[40px] bg-background">
                <div className="items-center mt-20">
                  <div className="w-52 h-72 flex-col justify-start items-start gap-10 inline-flex">
                    <div className="self-stretch h-10 flex-col justify-start items-start gap-1 flex">
                      <div className="self-stretch text-[#3c3c3c] text-base font-bold font-['Inter']">0.79%</div>
                      <div className="self-stretch text-[#3c3c3c] text-sm font-normal font-['Inter']">Середній коефіцієнт залучення</div>
                    </div>
                    <div className="self-stretch h-10 flex-col justify-start items-start gap-1 flex">
                      <div className="self-stretch text-[#3c3c3c] text-base font-bold font-['Inter']">0.83%</div>
                      <div className="self-stretch text-[#3c3c3c] text-sm font-normal font-['Inter']">Середній коефіцієнт залучення</div>
                    </div>
                    <div className="self-stretch h-10 flex-col justify-start items-start gap-1 flex">
                      <div className="self-stretch text-[#3c3c3c] text-base font-bold font-['Inter']">0.24%</div>
                      <div className="self-stretch text-[#3c3c3c] text-sm font-normal font-['Inter']">Середній коефіцієнт залучення</div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default StatisticsPage;