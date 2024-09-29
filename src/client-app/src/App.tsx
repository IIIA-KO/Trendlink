import './App.css'
import {BrowserRouter, Routes, Route, Navigate} from "react-router-dom";
import { AuthProvider } from './context/AuthContext';
import ProtectedRoute from './components/Routes/ProtectedRoute';
import {lazy, Suspense} from "react";

const  AuthPage = lazy(() =>import("./pages/AuthPage"));
const  CallBackPage = lazy(() =>import("./pages/CallBackPage"));
const  ProfilePage = lazy(() =>import("./pages/ProfilePage"));
const  NotFoundPage = lazy(() =>import("./pages/NotFoundPage"));
const ServerErrorPage = lazy(() =>import("./pages/ServerErrorPage"));
const LoadingPage = lazy(() =>import("./pages/LoadingPage"));

const App: React.FC = () => {
    return (
        <BrowserRouter>
            <AuthProvider>
                <Suspense fallback={<LoadingPage/>}>
                    <Routes>
                        <Route path="/login" element={<AuthPage/>} />
                        <Route path="/login/callback" element={<CallBackPage />}/>
                        <Route path="/" element={
                            <ProtectedRoute>
                                <ProfilePage />
                            </ProtectedRoute>
                        }/>
                        <Route path="/404" element={<NotFoundPage />} />
                        <Route path="*" element={<Navigate to="/404" replace />} />
                        <Route path="/500" element={<ServerErrorPage />} />
                    </Routes>
                </Suspense>
            </AuthProvider>
        </BrowserRouter>
    );
}

export default App;