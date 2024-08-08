export default function Login() {
    const getUserCodeForLoginFromGoogle = () => {
        const clientId = '1066993821532-be87irda6e602fr01fnktckokhpcucp7.apps.googleusercontent.com';
        const redirectUri = 'http://localhost:3000/auth/callback';
        const scope = 'openid profile email';
        const responseType = 'code';
        
        const authUrl = `https://accounts.google.com/o/oauth2/v2/auth?client_id=${clientId}&redirect_uri=${redirectUri}&response_type=${responseType}&scope=${scope}`;

        window.location.href = authUrl;
    };

    const getUserCodeForRegisterFromGoogle = () => {
        const clientId = '1066993821532-be87irda6e602fr01fnktckokhpcucp7.apps.googleusercontent.com';
        const redirectUri = 'http://localhost:3000/auth/callback';
        const scope = 'openid profile email';
        const responseType = 'code';

        // Passing parameters required for registration as query parameters
        const birthDate = '1990-01-01';
        const phoneNumber = '0123456789';
        const stateId = '00193234-0959-4278-903a-b99df10ffd9e';

        const authUrl = `https://accounts.google.com/o/oauth2/v2/auth?client_id=${clientId}&redirect_uri=${redirectUri}&response_type=${responseType}&scope=${scope}&state=${encodeURIComponent(`birthDate=${birthDate}&phoneNumber=${phoneNumber}&stateId=${stateId}`)}`;

        console.log("Redirecting to:", authUrl);

        window.location.href = authUrl;
    };

    return (
        <>
            <div>
                <h1>Login</h1>

                <button onClick={getUserCodeForLoginFromGoogle}>Login with Google</button>
            </div>

            <hr />

            <div>
                <h1>Register</h1>

                <button onClick={getUserCodeForRegisterFromGoogle}>Register with Google</button>
            </div>
        </>
    );
};