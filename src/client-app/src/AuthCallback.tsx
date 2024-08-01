import queryString from 'query-string';
import { useEffect } from 'react';
import { redirect, useNavigate } from 'react-router-dom';

const AuthCallback = () => {
    const navigate = useNavigate();

    useEffect(() => {
        const fetchToken = async () => {
            const params = queryString.parse(window.location.search);
            const code = params.code;

            if (code) {
                try {
                    const response = await fetch('https://localhost:5001/api/users/google-login', {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json',
                        },
                        body: JSON.stringify({
                            accessToken: code,
                        }),
                    });

                    console.log('Response:', response);

                    if (response.ok) {
                        const data = await response.json();
                        localStorage.setItem('access_token', data.access_token);
                        alert("You're logged in!");
                    } else {
                        console.error('Failed to exchange code for token');
                    }
                } catch (error) {
                    console.error('Error:', error);
                }
            }
        };

        fetchToken();
    }, [navigate]);

    return <div>Loading...</div>;
};

export default AuthCallback;
