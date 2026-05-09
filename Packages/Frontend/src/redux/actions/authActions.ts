import { createAsyncThunk } from '@reduxjs/toolkit';
import axiosInstance from '../../api/axiosInstance';
import { AUTH_API } from '@/api/auth/authAPI';
import { LoginRequest, RegisterRequest } from '@/api/auth/auth.type';

export const authActions = {
    login: createAsyncThunk<any, LoginRequest>('login', async (request) => {
        const response = await axiosInstance.post(AUTH_API.LOGIN, request);
        console.log('response:', response);
        return response.data;
    }),
    register: createAsyncThunk<any, RegisterRequest>('register', async (request) => {
        const response = await axiosInstance.post(AUTH_API.REGISTER, request);
        return response.data;
    }),
    getCurrentUser: createAsyncThunk('me', async () => {
        const response = await axiosInstance.get(AUTH_API.ME, { passError: true });
        return response.data;
    }),
    logout: createAsyncThunk("logout",async () =>{
        const response = await axiosInstance.get(AUTH_API.LOGOUT, { passError: true });
        return response.data;
    })

};
