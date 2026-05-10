import { CreatePostInput } from '@/components/Forms/CreatePostForm';
import axiosInstance from '../axiosInstance';
import { POST_API } from './postAPI';

export const postNewFeed = async (request: CreatePostInput) => {
    const response = await axiosInstance.post(POST_API.CREATE_POST, request);
    return response.data;
};

export const getPost = async (request: any) => {
    const response = await axiosInstance.get(POST_API.GET_POST, request)
    return response.data
}
