import { useEffect } from "react";

const Login = () => {
    useEffect(() => {
        console.log('Login component mounted');
    }, []);

    const handleGoogleLogin = () => {
        console.log("Login button clicked");
        
        const clientId = '1066993821532-be87irda6e602fr01fnktckokhpcucp7.apps.googleusercontent.com';
        const redirectUri = 'http://localhost:3000/auth/callback';
        const scope = 'openid profile email';
        const responseType = 'code';
        const authUrl = `https://accounts.google.com/o/oauth2/v2/auth?client_id=${clientId}&redirect_uri=${redirectUri}&response_type=${responseType}&scope=${scope}`;

        console.log("Redirecting to:", authUrl);

        window.location.href = authUrl;
    };

    return (
        <div>
            <h1>Login</h1>
            <button onClick={handleGoogleLogin}>Login with Google</button>
        </div>
    );
};

export default Login;