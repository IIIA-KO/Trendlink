import './App.css'
import { BrowserRouter, Routes, Route } from "react-router-dom";
import { AuthProvider } from './context/AuthContext';
import LoginPage from "./pages/LoginPage";
import RegisterPage from "./pages/RegisterPage";
import HomePage from "./pages/HomePage";
import ProtectedRoute from './components/ProtectedRoute';

const App: React.FC = () => {
  return (
      <BrowserRouter>
          <AuthProvider>
              <Routes>
                  <Route path="/login" element={<LoginPage />} />
                  <Route path="/register" element={<RegisterPage />} />
                  <Route path="/" element={
                      <ProtectedRoute>
                          <HomePage />
                      </ProtectedRoute>
                  }
                  />
              </Routes>
          </AuthProvider>
      </BrowserRouter>
  );
}

export default App
