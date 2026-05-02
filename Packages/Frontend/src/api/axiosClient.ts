import axios, { AxiosInstance, AxiosRequestConfig, AxiosResponse } from 'axios';

// 1. Khởi tạo cấu hình cơ bản
const axiosClient: AxiosInstance = axios.create({
    baseURL: 'http://localhost:5167/api', // Đúng Port Backend của bạn
    headers: {
        'Content-Type': 'application/json',
    },
});

// 2. Interceptor cho Request: Tự động gắn Token vào Header
axiosClient.interceptors.request.use(
    (config) => {
        // Lấy token từ localStorage mà bạn đã lưu ở trang Login
        const token = localStorage.getItem('token');
        if (token && config.headers) {
            // Gắn vào header Authorization theo chuẩn Bearer
            config.headers.Authorization = `Bearer ${token}`;
        }
        return config;
    },
    (error) => {
        return Promise.reject(error);
    },
);

// 3. Interceptor cho Response: Xử lý dữ liệu trả về và lỗi tập trung
axiosClient.interceptors.response.use(
    (response: AxiosResponse) => {
        // Trả về thẳng dữ liệu bên trong để code ở các Service gọn hơn
        return response.data;
    },
    (error) => {
        // Nếu lỗi 401 (Hết hạn token hoặc chưa login), có thể đá user ra trang Login
        if (error.response && error.response.status === 401) {
            localStorage.removeItem('token');
            // window.location.href = '/login';
        }
        return Promise.reject(error);
    },
);

export default axiosClient;
