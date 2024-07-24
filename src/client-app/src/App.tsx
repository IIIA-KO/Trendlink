import './App.css'
import LoginPage from "./Pages/LoginPage.tsx";
//import RegisterPage from "./pages/RegisterPage.tsx";
import HomePage from "./Pages/HomePage.tsx";
import { Navigate, BrowserRouter, Routes, Route } from "react-router-dom";
import {AuthProvider, useAuth} from './context/AuthContext';
//import PrivateRoute from "./components/PrivateRoute.tsx";

const ProtectedRoute: React.FC<{ children: React.ReactNode }> = ({ children }) => {
    const { isAuthenticated } = useAuth();
    return isAuthenticated ? <>{children}</> : <Navigate to="/login" />;
};

function App() {
  return (
      <AuthProvider>
          <BrowserRouter>
              <Routes>
                  <Route path="/login" element={<LoginPage />} />
                  <Route
                      path="/"
                      element={
                          <ProtectedRoute>
                              <HomePage />
                          </ProtectedRoute>
                      }
                  />
              </Routes>
          </BrowserRouter>
      </AuthProvider>
  );
}

export default App
