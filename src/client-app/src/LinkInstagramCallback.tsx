import queryString from 'query-string';
import { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

const LinkInstagramCallback = () => {
    const navigate = useNavigate();

    useEffect(() => {
        const fetchToken = async () => {
            const params = queryString.parse(window.location.search);
            const code = params.code;

            if (code) {
                try {
                    const response = await fetch('https://localhost:5001/api/users/link-instagram', {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json',
                        },
                        body: JSON.stringify({
                            code: code,
                        }),
                    });

                    console.log('Response:', response);

                    if (response.ok) {
                        alert("Instagram account linked");
                        navigate('/');
                    } else {
                        console.error('Failed to link Instagram account');
                    }
                } catch (error) {
                    console.error('Error:', error);
                }
            }
        }

        fetchToken();
    }, [navigate]);

    return (
        <>
            <div>Loading...</div>
        </>
    );
}

export default LinkInstagramCallback;