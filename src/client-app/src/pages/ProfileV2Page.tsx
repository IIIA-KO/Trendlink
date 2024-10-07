import React from "react";
import Navbar from "../components/Navbar";
import TopBar from "../components/TopBar";

const ProfileV2Page: React.FC = () => {

    return (
            <div className="bg-background relative flex justify-start items-center pl-4 md:pl-8 h-screen w-screen">
                <Navbar/>
                <TopBar />
            </div>

    );
};

export default ProfileV2Page;