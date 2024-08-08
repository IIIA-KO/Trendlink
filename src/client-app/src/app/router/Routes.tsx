import { createBrowserRouter, RouteObject } from "react-router-dom";
import Login from '../../Login';
import LoginCallback from "../../LoginCallback";
import App from "../../App";
import Home from "../../Home";
import LinkInstagram from "../../LinkInstagram";
import LinkInstagramCallback from "../../LinkInstagramCallback";

export const routes: RouteObject[] = [
    {
        path: "/",
        element: <App />,
        children: [
            { path: "", element: <Home /> },
            { path: "login", element: <Login /> },
            { path: "link-instagram", element: <LinkInstagram /> },
            { path: "login/callback", element: <LoginCallback /> },
            { path: "ling-instagram/callback", element: <LinkInstagramCallback /> },
        ]
    }
]

export const router = createBrowserRouter(routes);