import { Outlet } from 'react-router-dom';
import {DataProvider} from "../provider/DataProvider";

const InstLayout: React.FC = () => {
    return (
        <DataProvider>
            <div>
                <Outlet />
            </div>
        </DataProvider>
    );
}

export default InstLayout;