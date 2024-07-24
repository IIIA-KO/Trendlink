import './App.css'
import LoginPage from "./pages/LoginPage.tsx";
//import RegisterPage from "./pages/RegisterPage.tsx";
import HomePage from "./pages/HomePage.tsx";
import {BrowserRouter as Router, Navigate, Route, Routes} from "react-router-dom";
import {AuthProvider, useAuth} from './context/AuthContext';
//import PrivateRoute from "./components/PrivateRoute.tsx";

const ProtectedRoute: React.FC<{ children: React.ReactNode }> = ({ children }) => {
    const { isAuthenticated } = useAuth();
    return isAuthenticated ? <>{children}</> : <Navigate to="/login" />;
};

function App() {
  return (
      <AuthProvider>
          <Router>
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
          </Router>
      </AuthProvider>
  );
}

export default App
