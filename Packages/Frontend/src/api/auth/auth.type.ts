import { GenderTypeEnum } from '@/common/genderTypeEnum';
import { RoleTypeEnum } from '@/common/roleTypeEnum';

export type RegisterRequest = {
    email: string;
    firstName: string;
    lastName: string;
    roles: RoleTypeEnum[];
    birthday: string;
    gender: GenderTypeEnum;
    password: string;
};

export type LoginRequest = {
    email: string;
    password: string;
};
