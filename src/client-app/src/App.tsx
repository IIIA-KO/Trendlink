import './App.css'
import { BrowserRouter, Routes, Route } from "react-router-dom";
import { AuthProvider } from './context/AuthContext';
import AuthPage from "./Pages/AuthPage.tsx";
import HomePage from "./Pages/HomePage";
import ProtectedRoute from './components/ProtectedRoute';

const App: React.FC = () => {
  return (
      <BrowserRouter>
          <AuthProvider>
              <Routes>
                  <Route path="/login" element={<AuthPage />} />
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
