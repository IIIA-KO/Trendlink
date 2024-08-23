
import React, {useEffect} from 'react';

const GoogleLoginForm: React.FC = () => {

    useEffect(() => {
        const clientId = import.meta.env.VITE_GOOGLE_CLIENT_ID;
        const redirectUri = 'https://localhost:3000/login/callback';
        const scope = 'openid profile email';
        const responseType = 'code'

        const authUrl = `https://accounts.google.com/o/oauth2/v2/auth?client_id=${clientId}&redirect_uri=${redirectUri}&response_type=${responseType}&scope=${scope}`;
        window.location.href = authUrl;
    }, []);
    return null;
};

export default GoogleLoginForm;