import { Outlet } from 'react-router-dom';
import Navbar from '../components/Navbar';
import {DataProvider} from "../provider/DataProvider";

const MainLayout: React.FC = () => {
    return (
        <DataProvider>
            <div className="bg-background flex justify-start h-auto w-auto">
                <Navbar />
                <div className="w-5/6 h-auto">
                    <Outlet />
                </div>
            </div>
        </DataProvider>
    );
}

export default MainLayout;