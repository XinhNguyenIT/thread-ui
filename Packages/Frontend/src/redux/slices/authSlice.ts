import { createSlice } from '@reduxjs/toolkit';
import { authActions } from '@/redux/actions/authActions';
import { RoleTypeEnum } from '@/common/roleTypeEnum';
import { GenderTypeEnum } from '@/common/genderTypeEnum';

type userInfo = {
    email: string;
    firstName: string;
    lastName: string;
    id: number;
    role: RoleTypeEnum[];
    gender: GenderTypeEnum;
    birthday: string;
    avatarSrc: string;
};

const AuthSlice = createSlice({
    name: 'auth',
    initialState: {
        information: null as userInfo | null,
        isAuthenticated: false,
    },
    reducers: {
        logout: (state) => {
            state.information = null;
            state.isAuthenticated = false;
        },
    },
    extraReducers: (builder) =>
        builder
            .addCase(authActions.login.fulfilled, (state, action) => {
                const response = action.payload;
                state.information = response.data;
                state.isAuthenticated = true;
            })
            .addCase(authActions.login.pending, (state) => {
                state.information = null;
                state.isAuthenticated = false;
            })
            .addCase(authActions.login.rejected, (state) => {
                state.information = null;
                state.isAuthenticated = false;
            })
            .addCase(authActions.register.fulfilled, (state, action) => {
                const response = action.payload;
                state.information = response.data;
                state.isAuthenticated = true;
            })
            .addCase(authActions.register.pending, (state) => {
                state.information = null;
                state.isAuthenticated = false;
            })
            .addCase(authActions.register.rejected, (state) => {
                state.information = null;
                state.isAuthenticated = false;
            })
            .addCase(authActions.getCurrentUser.fulfilled, (state, action) => {
                var response = action.payload;
                state.information = response.data;
                state.isAuthenticated = true;
            })
            .addCase(authActions.getCurrentUser.pending, (state) => {
                state.information = null;
                state.isAuthenticated = false;
            })
            .addCase(authActions.getCurrentUser.rejected, (state) => {
                state.information = null;
                state.isAuthenticated = false;
            })
            .addCase(authActions.logout.fulfilled, (state) => {
                state.information = null;
                state.isAuthenticated = false;
            })
});

export default AuthSlice.reducer;
export const { logout } = AuthSlice.actions;
