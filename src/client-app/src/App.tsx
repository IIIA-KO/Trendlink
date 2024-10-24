import './App.css'
import {Routes, Route, Navigate} from "react-router-dom";
import { AuthProvider } from './context/AuthContext';
import ProtectedRoute from './components/Routes/ProtectedRoute';
import {Suspense} from "react";
import LoadingPage from "./pages/LoadingPage";
import LinkInstagramPage from './pages/LinkInstagramPage';
import LinkInstagramCallbackPage from './pages/LinkInstagramCallbackPage';
import MainLayout from './Layouts/MainLayout';
import AuthLayout from './Layouts/AuthLayout';
import StatisticsPage from "./pages/StatisticsPage";
import ProfilePage from "./pages/ProfilePage";
import TermsOfCooperationPage from "./pages/TermsOfCooperationPage";
import SearchBloggersPage from "./pages/SearchBloggersPage";
import AuthPage from "./pages/AuthPage";
import CallBackPage from "./pages/CallBackPage";
import LogoutPage from "./pages/LogoutPage";
import NotFoundPage from "./pages/NotFoundPage";
import ServerErrorPage from "./pages/ServerErrorPage";
import ProfileBloggersPage from "./pages/ProfileBloggerPage";
import CalendarPage from "./pages/CalendarPage";
import ReviewsPage from "./pages/ReviewsPage";
import EditProfilePage from "./pages/EditProfilePage";
import NotificationPage from "./pages/NotificationPage";

const App: React.FC = () => {
    return (
        <AuthProvider>
            <Suspense fallback={<LoadingPage/>}>
                <Routes>
                    <Route element={<MainLayout />}>
                        <Route path="/" element={<ProtectedRoute><ProfilePage/></ProtectedRoute>} />
                        <Route index path="/profile" element={<ProtectedRoute><ProfilePage/></ProtectedRoute>} />
                        <Route path="/profile/:userId" element={<ProfileBloggersPage />} />
                        <Route path="/statistics" element={<ProtectedRoute><StatisticsPage/></ProtectedRoute>} />
                        <Route path="/termsofcooperation" element={<ProtectedRoute><TermsOfCooperationPage/></ProtectedRoute>} />
                        <Route path="/searchbloggers" element={<ProtectedRoute><SearchBloggersPage/></ProtectedRoute>} />
                        <Route path="/calendar" element={<ProtectedRoute><CalendarPage /></ProtectedRoute>} />
                        <Route path="/reviews" element={<ProtectedRoute><ReviewsPage /></ProtectedRoute>} />
                        <Route path="/editprofile" element={<EditProfilePage/>} />
                        <Route path="/notifications" element={<NotificationPage/>} />
                    </Route>

                    <Route element={<AuthLayout />}>
                        <Route path="/login" element={<AuthPage/>} />
                        <Route path="/login/callback" element={<CallBackPage/>} />
                        <Route path='/link-instagram' element={<ProtectedRoute><LinkInstagramPage /></ProtectedRoute>} />
                        <Route path='/link-instagram/callback' element={<ProtectedRoute><LinkInstagramCallbackPage /></ProtectedRoute>} />
                        <Route path="/logout" element={<LogoutPage/>} />
                    </Route>

                    <Route path="/404" element={<NotFoundPage />} />
                    <Route path="/500" element={<ServerErrorPage />} />
                    <Route path="*" element={<Navigate to="/404" replace />} />
                </Routes>
            </Suspense>
        </AuthProvider>
    );
}

export default App;