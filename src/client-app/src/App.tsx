import './App.css'
import {BrowserRouter, Routes, Route, Navigate} from "react-router-dom";
import { AuthProvider } from './context/AuthContext';
import ProtectedRoute from './components/Routes/ProtectedRoute';
import {lazy, Suspense} from "react";
import LoadingPage from "./pages/LoadingPage";
import {ProfileProvider} from "./context/ProfileContext";
import LinkInstagramPage from './pages/LinkInstagramPage';
import LinkInstagramCallbackPage from './pages/LinkInstagramCallbackPage';

const AuthPage = lazy(() =>import("./pages/AuthPage"));
const CallBackPage = lazy(() =>import("./pages/CallBackPage"));
const ProfilePage = lazy(() =>import("./pages/ProfilePage"));
const NotFoundPage = lazy(() =>import("./pages/NotFoundPage"));
const ServerErrorPage = lazy(() =>import("./pages/ServerErrorPage"));
const StatisticsPage = lazy(() =>import("./pages/StatisticsPage"));
const LogoutPage = lazy(() =>import("./pages/LogoutPage"));
const TermsOfCooperationPage = lazy(() =>import("./pages/TermsOfCooperationPage"));
const SearchBloggersPage = lazy(() =>import("./pages/SearchBloggersPage"));
const ProfileBloggersPage = lazy(()=>import("./pages/ProfileBloggerPage"));

const App: React.FC = () => {
    return (
        <BrowserRouter>
            <AuthProvider>
                <ProfileProvider>
                    <Suspense fallback={<LoadingPage/>}>
                        <Routes>
                            <Route path="/" element={<ProfileProvider><ProtectedRoute><ProfilePage/></ProtectedRoute></ProfileProvider>} />
                            <Route path="/profile/:userId" element={<ProfileBloggersPage />} />
                            <Route path="/login" element={<AuthPage/>} />
                            <Route path="/login/callback" element={<CallBackPage/>} />
                            <Route path="/statistics" element={<ProfileProvider><ProtectedRoute><StatisticsPage/></ProtectedRoute></ProfileProvider>} />
                            <Route path="/termsofcooperation" element={<ProfileProvider><ProtectedRoute><TermsOfCooperationPage/></ProtectedRoute></ProfileProvider>} />
                            <Route path="/searchbloggers" element={<ProfileProvider><ProtectedRoute><SearchBloggersPage/></ProtectedRoute></ProfileProvider>} />
                            <Route path="/logout" element={<LogoutPage/>} />
                            <Route path="/loading" element={<LoadingPage/>} />
                            <Route path="/login" element={<AuthPage />} />
                            <Route path="/login/callback" element={<CallBackPage />} />
                            <Route path='/link-instagram' element={<ProfileProvider><ProtectedRoute><LinkInstagramPage /></ProtectedRoute></ProfileProvider>} />
                            <Route path='/link-instagram/callback' element={<ProtectedRoute><LinkInstagramCallbackPage /></ProtectedRoute>} />
                            <Route path="/404" element={<NotFoundPage />} />
                            <Route path="/500" element={<ServerErrorPage />} />
                            <Route path="*" element={<Navigate to="/404" replace />} />
                        </Routes>
                    </Suspense>
                </ProfileProvider>
            </AuthProvider>
        </BrowserRouter>
    );
}

export default App;