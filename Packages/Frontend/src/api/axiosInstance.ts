import axios, { AxiosError, InternalAxiosRequestConfig } from 'axios';
import { logout } from '@/redux/slices/authSlice';
import { handleErrorRequest, handleSuccessStatus } from '@/redux/slices/statusSlice';
import { AppStore } from '@/redux/store';

const axiosInstance = axios.create({
    baseURL: import.meta.env.VITE_API_URL,
    withCredentials: true,
    headers: {
        'Content-Type': 'application/json',
    },
});

let store: AppStore | undefined;

export const injectStore = (_store: AppStore) => {
    store = _store;
};

axiosInstance.interceptors.response.use(
    async (response) => {
        if (response.config.showSuccess && store) {
            store.dispatch(handleSuccessStatus(response.data.message));
        }
        return response;
    },
    async (error: AxiosError<any>) => {
        const originalRequest = error.config as InternalAxiosRequestConfig & { _retry?: boolean };
        if (error.response?.status === 401 && !originalRequest._retry) {
            originalRequest._retry = true;
            try {
                await axios.post(
                    `${import.meta.env.VITE_API_URL}/api/Auth/refresh-token`,
                    {},
                    { withCredentials: true },
                );

                return axiosInstance.request(originalRequest);
            } catch (er) {
                if (store) {
                    store.dispatch(logout());
                }
                return Promise.reject(er);
            }
        }

        const responseError = error.response?.data || {
            message: 'Unexpected error',
        };

        if (!error.config?.passError && store) {
            store.dispatch(handleErrorRequest(responseError));
        }
        return Promise.reject(error);
    },
);

export default axiosInstance;
