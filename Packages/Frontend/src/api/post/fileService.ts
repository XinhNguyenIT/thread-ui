import { CreatePostInput } from '@/components/Forms/CreatePostForm';
import axiosInstance from '../axiosInstance';
import { POST_API } from './postAPI';

export const postNewFeed = async (request: CreatePostInput) => {
    const response = await axiosInstance.post(POST_API.CREATE_POST, request);
    return response.data;
};
