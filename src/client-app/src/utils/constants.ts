export const googleClientId = import.meta.env.VITE_GOOGLE_CLIENT_ID;
export const googleRedirectUri = 'https://localhost:3000/login/callback';
export const googleScope = 'openid profile email';
export const googleResponseType = 'code'

export const instagramClientId = import.meta.env.VITE_INSTAGRAM_CLIENT_ID;
export const instagramConfigId = import.meta.env.VITE_INSTAGRAM_CONFIG_ID;
export const instagramRedirectUri = 'https://localhost:3000/link-instagram/callback';
export const instagramScope = 'instagram_basic,instagram_manage_insights,instagram_manage_comments,pages_show_list,pages_read_engagement';
export const instagramResponseType = 'code';

export const accountCategories = [
    { id: 0, name: "None" },
    { id: 1, name: "Cooking and Food" },
    { id: 2, name: "Fashion and Style" },
    { id: 3, name: "Clothing and Footwear" },
    { id: 4, name: "Horticulture" },
    { id: 5, name: "Animals" },
    { id: 6, name: "Cryptocurrency" },
    { id: 7, name: "Technology" },
    { id: 8, name: "Travel" },
    { id: 9, name: "Education" },
    { id: 10, name: "Fitness" },
    { id: 11, name: "Art" },
    { id: 12, name: "Photography" },
    { id: 13, name: "Music" },
    { id: 14, name: "Sports" },
    { id: 15, name: "Health and Wellness" },
    { id: 16, name: "Gaming" },
    { id: 17, name: "Parenting" },
    { id: 18, name: "DIY and Crafts" },
    { id: 19, name: "Literature" },
    { id: 20, name: "Science" },
    { id: 21, name: "History" },
    { id: 22, name: "News" },
    { id: 23, name: "Politics" },
    { id: 24, name: "Finance" },
    { id: 25, name: "Environment" },
    { id: 26, name: "Real Estate" },
    { id: 27, name: "Automobiles" },
    { id: 28, name: "Movies and TV" },
    { id: 29, name: "Comedy" },
    { id: 30, name: "Home Decor" },
    { id: 31, name: "Relationships" },
    { id: 32, name: "Self Improvement" },
    { id: 33, name: "Entrepreneurship" },
    { id: 34, name: "Legal Advice" },
    { id: 35, name: "Marketing" },
    { id: 36, name: "Mental Health" },
    { id: 37, name: "Personal Development" },
    { id: 38, name: "Religion and Spirituality" },
    { id: 39, name: "Social Media" },
];