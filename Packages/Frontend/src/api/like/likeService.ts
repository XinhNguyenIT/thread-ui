import axiosInstance from "../axiosInstance";
import { LIKE_API } from "./likeAPI";

export const interactLikeAction = async (request: any) => {
    const response = await axiosInstance.post(LIKE_API.CRUD_LIKE, request);
    return response.data;
}