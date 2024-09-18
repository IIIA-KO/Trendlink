import axios, { AxiosError } from 'axios';
import { toast } from 'react-toastify';

export const handleError = (error: unknown): void => {
    if (axios.isAxiosError(error)) {
        const err = error.response;

        if (Array.isArray(err?.data?.errors)) {
            for (const val of err.data.errors) {
                toast.warning(val.description);
            }
        }

        else if (err?.data?.errors && typeof err.data.errors === 'object') {
            for (const key in err.data.errors) {
                if (Object.prototype.hasOwnProperty.call(err.data.errors, key)) {
                    toast.warning(err.data.errors[key][0]);
                }
            }
        }

        else if (err?.data) {
            toast.warning(err.data);
        }

        else if (err?.status === 401) {
            toast.warning('Please login');
            window.history.pushState({}, 'LoginPage', '/login');
        }

        else {
            toast.warning(err?.data || 'An error occurred');
        }
    } else if (error instanceof Error) {
        toast.error(error.message);
    } else {
        toast.error('An unknown error occurred');
        console.error("Unknown error: ", error);
    }
};