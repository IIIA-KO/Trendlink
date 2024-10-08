import './App.css'
import {BrowserRouter, Routes, Route, Navigate} from "react-router-dom";
import { AuthProvider } from './context/AuthContext';
import ProtectedRoute from './components/Routes/ProtectedRoute';
import {lazy, Suspense} from "react";
import LoadingPage from "./pages/LoadingPage";
import {ProfileProvider} from "./context/ProfileContext";

const AuthPage = lazy(() =>import("./pages/AuthPage"));
const CallBackPage = lazy(() =>import("./pages/CallBackPage"));
const ProfilePage = lazy(() =>import("./pages/ProfilePage"));
const NotFoundPage = lazy(() =>import("./pages/NotFoundPage"));
const ServerErrorPage = lazy(() =>import("./pages/ServerErrorPage"));

const App: React.FC = () => {
    return (
        <BrowserRouter>
            <AuthProvider>
                <Suspense fallback={<LoadingPage/>}>
                    <Routes>
                        <Route path="/" element={<ProtectedRoute><ProfileProvider><ProfilePage/></ProfileProvider></ProtectedRoute>} />
                        <Route path="/login" element={<AuthPage/>} />
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