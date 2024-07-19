import './App.css'
import LoginPage from "./Pages/LoginPage.tsx";
import RegisterPage from "./Pages/RegisterPage.tsx";
import HomePage from "./Pages/HomePage.tsx";
import {BrowserRouter, Route, Routes} from "react-router-dom";

function App() {
  return (
      <div>
          <BrowserRouter>
              <Routes>
                  <Route index path="/" element={<HomePage/>}/>
                  <Route path="/login" element={<LoginPage/>} />
                  <Route path="/register" element={<RegisterPage/>} />
              </Routes>
          </BrowserRouter>
      </div>
  );
}

export default App
