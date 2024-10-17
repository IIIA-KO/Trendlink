export const googleClientId = import.meta.env.VITE_GOOGLE_CLIENT_ID;
export const googleRedirectUri = 'https://localhost:3000/login/callback';
export const googleScope = 'openid profile email';
export const googleResponseType = 'code'

export const instagramClientId = import.meta.env.VITE_INSTAGRAM_CLIENT_ID;
export const instagramConfigId = import.meta.env.VITE_INSTAGRAM_CONFIG_ID;
export const instagramRedirectUri = 'https://localhost:3000/link-instagram/callback';
export const instagramScope = 'instagram_basic,instagram_manage_insights,instagram_manage_comments,pages_show_list,pages_read_engagement';
export const instagramResponseType = 'code';