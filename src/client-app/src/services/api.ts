import axios from 'axios';
import useAuth from "../hooks/useAuth";

const apiURL = import.meta.env.VITE_API_URL;

const axiosInstance  = axios.create({
    baseURL:  apiURL,
    withCredentials: true,
    headers: {
        'Content-Type': 'application/json',
    },
    timeout: 10000,
});

axiosInstance.interceptors.request.use(request => {
    const token = localStorage.getItem('accessToken');
    if (token) {
        request.headers.Authorization = `Bearer ${token}`;
    }
    return request;
}, error => {
    return Promise.reject(error);
});

let isRefreshing = false;
let refreshSubscribers: ((token: string) => void)[] = [];

const onRefreshed = (accessToken: string) => {
    refreshSubscribers.map((callback) => callback(accessToken));
    refreshSubscribers = [];
};

const addRefreshSubscriber = (callback: (token: string) => void) => {
    refreshSubscribers.push(callback);
};

export const setupInterceptors = (authContext: ReturnType<typeof useAuth>) => {
    axiosInstance.interceptors.response.use(
        response => response,
        async (error) => {
            const { response, config } = error;
            const originalRequest = config;

            if (response && response.status === 500) {
                authContext.navigate('/500');
            }

            if (response && response.status === 401 && !originalRequest._retry) {
                if (!isRefreshing) {
                    isRefreshing = true;
                    try {
                        const newTokens = await authContext.refreshTokens();
                        if (newTokens) {
                            isRefreshing = false;
                            onRefreshed(localStorage.getItem('accessToken')!);
                        } else {
                            isRefreshing = false;
                            authContext.logout();
                        }
                    } catch (e) {
                        isRefreshing = false;
                        authContext.logout();
                        return Promise.reject(e);
                    }
                }

                return new Promise((resolve) => {
                    addRefreshSubscriber((accessToken) => {
                        originalRequest.headers.Authorization = `Bearer ${accessToken}`;
                        originalRequest._retry = true;
                        resolve(axiosInstance(originalRequest));
                    });
                });
            }

            return Promise.reject(error);
        }
    );
};


export default axiosInstance;

