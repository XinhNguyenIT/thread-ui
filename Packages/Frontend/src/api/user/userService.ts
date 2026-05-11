import { GenderTypeEnum } from "@/common/genderTypeEnum";
import axiosInstance from "../axiosInstance";
import { USER_API } from "./userAPI";

export interface UpdateUserRequest {
    firstName: string;
    lastName: string;
    gender: GenderTypeEnum;
}

export interface UpdateAvatarRequest {
    file: string
    privacy: "PRIVATE" | "PUBLIC";
}

export const updateAvatar = async (request: UpdateAvatarRequest) => {
    const response = await axiosInstance.put(USER_API.UPDATE_AVATAR, request);
    return response.data;
}

export const updateUserProfile = async (request: UpdateUserRequest) => {
    const response = await axiosInstance.put(USER_API.UPDATE_USER_PROFILE, request);
    return response.data;
}