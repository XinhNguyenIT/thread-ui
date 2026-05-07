import { GenderTypeEnum } from "@/common/genderTypeEnum";
import { RoleTypeEnum } from "@/common/roleTypeEnum";

// tham chiếu tạm thời tới type trong Packages\Frontend\src\redux\slices\authSlice.ts
export type UserInfo = {
    email: string;
    firstName: string;
    lastName: string;
    id: number;
    role: RoleTypeEnum[];
    gender: GenderTypeEnum;
    birthday: string;
    avatarSrc: string;
};