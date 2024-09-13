import './App.css'
import { BrowserRouter, Routes, Route } from "react-router-dom";
import { AuthProvider } from './context/AuthContext';
import AuthPage from "./pages/AuthPage";
import HomePage from "./pages/HomePage";
import ProtectedRoute from './components/ProtectedRoute';
import CallBackPage from "./pages/CallBackPage";
import '@fontsource/kodchasan';
import '@fontsource/inter'

const App: React.FC = () => {
  return (
      <BrowserRouter>
          <AuthProvider>
              <Routes>
                  <Route path="/login" element={<AuthPage />} />
                  <Route path="/login/callback" element={<CallBackPage />}/>
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

export default App;