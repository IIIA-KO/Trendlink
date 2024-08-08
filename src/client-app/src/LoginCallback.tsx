import queryString from 'query-string';
import { useEffect } from 'react';
import { redirect, useNavigate } from 'react-router-dom';

const LoginCallback = () => {
    const navigate = useNavigate();

    useEffect(() => {
        const fetchToken = async () => {
            const params = queryString.parse(window.location.search);
            const code = params.code;

            const state = params.state ? queryString.parse(params.state as string) : {};
            const birthDate = state.birthDate;
            const phoneNumber = state.phoneNumber;
            const stateId = state.stateId;

            console.log('Code:', code);
            console.log('BirthDate:', birthDate);
            console.log('PhoneNumber:', phoneNumber);
            console.log('StateId:', stateId);

            if (code) {
                try {
                    if (!birthDate || !phoneNumber || !stateId) {
                        const loginResponse = await fetch('https://localhost:5001/api/users/google-login', {
                            method: 'POST',
                            headers: {
                                'Content-Type': 'application/json',
                            },
                            body: JSON.stringify({
                                code: code,
                            }),
                        });

                        console.log('Response:', loginResponse);

                        if (loginResponse.ok) {
                            const data = await loginResponse.json();
                            console.log('Data:', data);
                            
                            localStorage.setItem('access_token', data.access_token);
                            alert('Login successful!');
                            navigate('/');
                        } else {
                            console.log('Response:', loginResponse);
                            console.error('Failed to exchange code for token');
                        }
                    } else {
                        const registerResponse = await fetch('https://localhost:5001/api/users/google-register', {
                            method: 'POST',
                            headers: {
                                'Content-Type': 'application/json',
                            },
                            body: JSON.stringify({
                                code: code,
                                birthDate: birthDate,
                                phoneNumber: phoneNumber,
                                stateId: stateId,
                            }),
                        });

                        console.log('Response:', registerResponse);

                        if (registerResponse.ok) {
                            const data = await registerResponse.json();
                            console.log('Data:', data);
                            
                            localStorage.setItem('access_token', data.access_token);
                            alert('Registration successful!');
                            navigate('/');
                        } else {
                            console.log('Response:', registerResponse);
                            console.error('Failed to exchange code for token');
                        }
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

export default LoginCallback;
