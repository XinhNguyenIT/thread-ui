import 'axios';

declare module 'axios' {
    export interface AxiosRequestConfig {
        showSuccess?: boolean;
        passError?: boolean;
    }
}
