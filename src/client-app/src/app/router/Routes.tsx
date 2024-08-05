import { createBrowserRouter, RouteObject } from "react-router-dom";
import Login from '../../Login';
import AuthCallback from "../../AuthCallback";
import App from "../../App";
import Home from "../../Home";

export const routes: RouteObject[] = [
    {
        path: "/",
        element: <App />,
        children: [
            { path: "", element: <Home /> },
            { path: "login", element: <Login /> },
            { path: "auth/callback", element: <AuthCallback /> },
        ]
    }
]

export const router = createBrowserRouter(routes);