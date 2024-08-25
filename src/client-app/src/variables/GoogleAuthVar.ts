export const clientId = import.meta.env.VITE_GOOGLE_CLIENT_ID;
export const redirectUri = 'https://localhost:3000/login/callback';
export const scope = 'openid profile email';
export const responseType = 'code'