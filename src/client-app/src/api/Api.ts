import axios from 'axios';

axios.defaults.baseURL = 'https://localhost:5001/api';

const axiosInstance  = axios.create({
    baseURL:  axios.defaults.baseURL,
    withCredentials: true,
    headers: {
        'Access-Control-Allow-Origin': '*',
        'Access-Control-Allow-Credentials': 'true',
        'Access-Control-Allow-Headers': 'content-type',
    },
    timeout: 900,
});

axiosInstance .interceptors.request.use(request => {
    const token = localStorage.getItem('accessToken');
    if (token) {
        request.headers.Authorization = `Bearer ${token}`;
    }
    return request;
}, error => {
    Promise.reject(error);
});

let isRefreshing = false;
let refreshSubscribers: ((token: string) => void)[] = [];

const onRefreshed = (accessToken: string) => {
    refreshSubscribers.map((callback) => callback(accessToken));
};

const addRefreshSubscriber = (callback: (token: string) => void) => {
    refreshSubscribers.push(callback);
};

axiosInstance.interceptors.response.use(
    (response) => response,
    async (error) => {

        if (!error.response) {
            console.error('Network/Server error', error);
            return Promise.reject(new Error('Network/Server error'));
        }

        const { config, response: { status } } = error;
        const originalRequest = config;

        if (status === 401) {
            if (!isRefreshing) {
                isRefreshing = true;
                const refreshToken = localStorage.getItem('refreshToken');
                try {
                    const response = await axios.post(`${axios.defaults.baseURL}/users/refresh`, { refreshToken });
                    const { accessToken } = response.data;
                    localStorage.setItem('accessToken', accessToken);
                    isRefreshing = false;
                    onRefreshed(accessToken);
                    refreshSubscribers = [];
                } catch (err) {
                    isRefreshing = false;
                    return Promise.reject(err);
                }
            }

            return new Promise((resolve) => {
                addRefreshSubscriber((accessToken) => {
                    originalRequest.headers.Authorization = `Bearer ${accessToken}`;
                    resolve(axiosInstance(originalRequest));
                });
            });
        }

        return Promise.reject(error);
    }
);

export default axiosInstance;