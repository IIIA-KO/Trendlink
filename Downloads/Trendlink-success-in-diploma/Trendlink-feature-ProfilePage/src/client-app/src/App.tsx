import './App.css'
import {BrowserRouter, Routes, Route, Navigate} from "react-router-dom";
import { AuthProvider } from './context/AuthContext';
import ProtectedRoute from './components/Routes/ProtectedRoute';
import {lazy, Suspense} from "react";
import LoadingPage from "./pages/LoadingPage";
import {ProfileProvider} from "./context/ProfileContext";
import EditPage from './pages/EditPage';

const CalendarPage = lazy(()=>import("./pages/CalendarPage"));
const AuthPage = lazy(() =>import("./pages/AuthPage"));
const CallBackPage = lazy(() =>import("./pages/CallBackPage"));
const ProfilePage = lazy(() =>import("./pages/ProfilePage"));
const NotFoundPage = lazy(() =>import("./pages/NotFoundPage"));
const ServerErrorPage = lazy(() =>import("./pages/ServerErrorPage"));
const StatisticsPage = lazy(() =>import("./pages/StatisticsPage"));
const LogoutPage = lazy(() =>import("./pages/LogoutPage"));
const ContractPage = lazy(() =>import("./pages/ContractPage"));
const Notifications = lazy(() =>import("./pages/Notifications"));

const App: React.FC = () => {
    return (
        <BrowserRouter>
            <AuthProvider>
                <Suspense fallback={<LoadingPage/>}>
                    <Routes>
                        <Route path="/" element={<ProtectedRoute><ProfileProvider><ProfilePage/></ProfileProvider></ProtectedRoute>} />
                        <Route path="/calendar" element={<ProfileProvider><CalendarPage/></ProfileProvider>} />
                        <Route path="/notifications" element={<ProfileProvider><Notifications/></ProfileProvider>} />
                        <Route path="/editpage" element={<ProfileProvider><EditPage/></ProfileProvider>} />
                        <Route path="/login/callback" element={<CallBackPage/>} />
                        <Route path="/statistics" element={<ProfileProvider><StatisticsPage/></ProfileProvider>} />
                        <Route path="/contract" element={<ProfileProvider><ContractPage/></ProfileProvider>} />
                        <Route path="/calendar" element={<ProfileProvider><CalendarPage/></ProfileProvider>} />
                        <Route path="/logout" element={<LogoutPage/>} />
                        <Route path="/loading" element={<LoadingPage/>} />
                        <Route path="/404" element={<NotFoundPage />} />
                        <Route path="/500" element={<ServerErrorPage />} />
                        <Route path="*" element={<Navigate to="/404" replace />} />
                    </Routes>
                </Suspense>
            </AuthProvider>
        </BrowserRouter>
    );
}

export default App;