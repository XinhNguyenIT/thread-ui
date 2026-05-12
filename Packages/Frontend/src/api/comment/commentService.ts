import axiosInstance from "../axiosInstance";
import { COMMENT_API } from "./commentAPI";

type GetCommentProps = {
    // commentId: number
    postId: number
}

type PostNewCommentProps = {
    commentId: number
    postId: number
    content: string
}

export const getComment = async (request: GetCommentProps) => {
    const response = await axiosInstance.get(`${COMMENT_API.GET_COMMENT}?PostId=${request.postId}`);
    return response.data;
}

export const postNewComment = async (request: PostNewCommentProps) => {
    const response = await axiosInstance.post(COMMENT_API.CREATE_COMMENT, request);
    return response.data;
}