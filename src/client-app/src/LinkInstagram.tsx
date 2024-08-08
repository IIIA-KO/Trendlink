const LinkInstagram = () => {
    const handleLinkInstagram = () => {
        const clientId = '488560280495895';
        const redirectUri = 'http://localhost:3000/link-instagram/callback';
        const scope = 'user_profile user_media';
        const responseType = 'code';

        const authUrl = `https://www.instagram.com/oauth/authorize?client_id=${clientId}&redirect_uri=${redirectUri}&scope=${scope}&response_type=${responseType}`;
        
        window.location.href = authUrl;
    }

    return (
        <div>
            <h1>Link Instagram</h1>
            <button onClick={handleLinkInstagram}>Link Instagram</button>
        </div>
    );
};

export default LinkInstagram;