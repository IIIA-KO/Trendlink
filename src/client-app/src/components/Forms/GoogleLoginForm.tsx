import React, {useEffect} from 'react';
import {googleClientId, googleRedirectUri, googleScope, googleResponseType} from "../../utils/constants";

const GoogleLoginForm: React.FC = () => {

    useEffect(() => {
        const authUrl = `https://accounts.google.com/o/oauth2/v2/auth?client_id=${googleClientId}&redirect_uri=${googleRedirectUri}&response_type=${googleResponseType}&scope=${googleScope}`;
        window.location.href = authUrl;
    }, []);
    return null;
};

export default GoogleLoginForm;