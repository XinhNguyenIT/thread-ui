import axiosInstance from '../axiosInstance';
import { FILE_API } from './fileAPI';

export const uploadFiles = async (file: File) => {
    const formData = new FormData();
    formData.append('file', file);

    const response = await axiosInstance.post(FILE_API.UPLOAD_FILE, formData, {
        headers: {
            'Content-Type': 'multipart/form-data',
        },
    });
    return response.data;
};

export const deleteFiles = async (fullPath: string) => {
    const response = await axiosInstance.delete(`${FILE_API.DELETE_FILE}?fullPath=${fullPath}`);
    return response.data;
};
