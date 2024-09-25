import axios from 'axios';

export const handleError = (error: unknown): void => {
    if (axios.isAxiosError(error)) {
        if (error.response) {
            console.error('Response error:', error.response.data.message);
            console.error('Status code:', error.response.status);
        } else if (error.request) {
            console.error('No response received:', error.request);
        } else {
            console.error('Axios setup error:', error.message);
        }
    } else if (error instanceof Error) {
        console.error('General error:', error.message);
    } else {
        console.error('Unknown error:', error);
    }
};