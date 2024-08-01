import './App.css'
import { useEffect } from 'react';
import { Outlet } from "react-router-dom";
function App() {
  useEffect(() => {
    console.log('App component mounted');
  }, []);

  return (
    <Outlet></Outlet>
  )
}

export default App;