import axiosClient from '../api/axiosClient'; // Dùng client chung thay vì axios gốc

export const register = (userData: any) => {
    return axiosClient.post('/Auth/register', userData);
    // Không cần viết lại http://localhost... và .data nữa
};

export const login = (credentials: any) => {
    return axiosClient.post('/Auth/login', credentials);
};
