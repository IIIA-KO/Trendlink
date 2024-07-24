import axios from 'axios';

const API_URL = 'http://localhost:5000';

export const login = async (email: string, password: string) => {
    const response = await axios.post(
        `${API_URL}/users/login`,
        { email, password },
        { withCredentials: true, }
    );
    if (response.data.token) {
        localStorage.setItem('authToken', response.data.token);
    }
    return response.data;
};

export const refreshToken = async () => {
    const token = getAuthToken();
    if (token) {
        const response = await axios.post(`${API_URL}/users/refresh`, {}, {
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });
        if (response.data.token) {
            localStorage.setItem('authToken', response.data.token);
        }
        return response.data;
    }
    throw new Error('No token available');
};

export const logout = () => {
    localStorage.removeItem('authToken');
    window.location.href = '/login'; // Перенаправлення
};

export const getAuthToken = () => {
    return localStorage.getItem('authToken');
};

// Налаштування для включення токена в заголовки
const axiosInstance = axios.create({
    baseURL: API_URL,
    withCredentials: true,
    headers: {
        'Access-Control-Allow-Origin': '*',
        'Access-Control-Allow-Credentials': 'true',
        'Access-Control-Max-Age': '1800',
        'Access-Control-Allow-Headers': 'content-type',
        'Access-Control-Allow-Methods': 'PUT, POST, GET, DELETE, PATCH, OPTIONS',
    }
});

axiosInstance.interceptors.request.use(
    (config) => {
        const token = getAuthToken();
        if (token) {
            config.headers['Authorization'] = `Bearer ${token}`;
        }
        return config;
    },
    (error) => {
        return Promise.reject(error);
    }
);

// Оновлення токену при отриманні відповіді 401
axiosInstance.interceptors.response.use(
    response => response,
    async error => {
        const originalRequest = error.config;
        if (error.response.status === 401 && !originalRequest._retry) {
            originalRequest._retry = true;
            try {
                await refreshToken();
                const token = getAuthToken();
                if (token) {
                    originalRequest.headers['Authorization'] = `Bearer ${token}`;
                }
                return axiosInstance(originalRequest);
            } catch (refreshError) {
                logout();
            }
        }
        return Promise.reject(error);
    }
);

export default axiosInstance;