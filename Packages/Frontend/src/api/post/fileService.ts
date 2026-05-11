import { CreatePostInput } from '@/components/Forms/CreatePostForm';
import axiosInstance from '../axiosInstance';
import { POST_API } from './postAPI';
import { GetPageRequest } from './post.type';

export const postNewFeed = async (request: CreatePostInput) => {
    const response = await axiosInstance.post(POST_API.CREATE_POST, request);
    return response.data;
};

export const getPost = async (request: GetPageRequest) => {
    const response = await axiosInstance.get(`${POST_API.GET_POST}?Page=${request.page}&PageSize=${request.pageSize}`)
    return response.data 
}

export const deletePost = async (request: number) => {
    const response = await axiosInstance.delete(`${POST_API.DELETE_POST}?PostId=${request}`);
    return response.data;
}