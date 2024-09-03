import React, {useEffect} from 'react';
import {clientId, redirectUri, scope, responseType} from "../../variables/GoogleAuthVar.ts";

const GoogleLoginForm: React.FC = () => {

    useEffect(() => {
        const authUrl = `https://accounts.google.com/o/oauth2/v2/auth?client_id=${clientId}&redirect_uri=${redirectUri}&response_type=${responseType}&scope=${scope}`;
        window.location.href = authUrl;
    }, []);
    return null;
};

export default GoogleLoginForm;