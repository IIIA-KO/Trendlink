import Navbar from "../components/Navbar";
import TopBar from "../components/TopBar";
import StatisticsBar from "../components/StatisticsBar";
import {useProfile} from "../hooks/useProfile";
import {useNavigate} from "react-router-dom";
import { useEffect, useMemo } from "react";
import { Table } from "antd";
import TableNotifications from "../components/TableNotifications"
import { date } from "yup";
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
                <div className="flex flex-col gap-2  bg-custom-bg bg-cover bg-no-repeat rounded-[50px] h-auto w-auto min-h-screen min-w-screen sm:mr-24 md:mr-32 lg:mr-42 xl:mr-64 mt-10">
                    <TopBar />
                   
                    <div className="h-1/4 w-full text-center text-black">
                    <div className="h-1/4 w-full inline-block text-center text-black">
                        <div className="h-full relative  w-1/2 mx-10 my-6">
                        <div style={{width: 299, height: 35, justifyContent: 'flex-start', alignItems: 'center', gap: 2, display: 'inline-flex'}}>
  <div style={{width: 35, height: 35, position: 'relative'}}>
    <div style={{width: 35, height: 35, left: 0, top: 0, position: 'absolute', background: '#FFD632', borderRadius: 9999}} />
    <div style={{left: 12, top: 8, position: 'absolute', textAlign: 'center', color: 'black', fontSize: 16, fontFamily: 'Inter', fontWeight: '400', wordWrap: 'break-word'}}>2</div>
  </div>
  <div style={{textAlign: 'center', color: '#3C3C3C', fontSize: 20, fontFamily: 'Inter', fontWeight: '700', textDecoration: 'underline', wordWrap: 'break-word'}}>Сповіщення на співпрацю</div>
</div>
                          </div>
                        </div>
                        <div className="h-full relative w-1/2 mx-10 my-6">
                          <TableNotifications></TableNotifications>
                          </div>
                      </div>
                </div>
            </div>
        </div>
    );
};

export default StatisticsPage;