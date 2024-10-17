import { useEffect, useRef, useState } from "react";
import { useNavigate } from "react-router-dom";
import { linkInstagram, renewInstagramAccess } from "../services/instagram";
const LinkInstagramCallbackPage = () => {
    const navigate = useNavigate();
    const [message, setMessage] = useState("Loading...");
    const isFetched = useRef(false);

    useEffect(() => {
        const fetchToken = async () => {
            if (isFetched.current) return;
            isFetched.current = true;

            setMessage("Linking Your Instagram Account...");

            const params = new URLSearchParams(window.location.search);
            const code = params.get('code');
            const accessToken = localStorage.getItem('access_token');

            if (code) {
                console.log('Code:', code);
                try {
                    const result = await linkInstagram(code);
                    if (result) {
                        setMessage("Instagram account linked successfully");
                        navigate('/');
                    } else {
                        setMessage('Failed to link Instagram account');
                    }
                } catch (error) {
                    console.error('Error:', error);
                    alert('An error occurred while linking the Instagram account.');
                }
            } else {
                alert('No code parameter found. Please try again.');
            }
        };

        fetchToken();
    }, [navigate]);

    return (
        <div className="flex items-center justify-center h-screen bg-gray-50">
            <div className="text-center">
                <p className="text-center text-[1.50rem] font-inter font-regular text-textSecondary mb-8">
                    {message}
                </p>
            </div>
        </div>
    );
}

export default LinkInstagramCallbackPage;