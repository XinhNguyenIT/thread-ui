import { createSlice } from '@reduxjs/toolkit';
import { authActions } from '@/redux/actions/authActions';

interface StatusState {
    isSuccess: boolean;
    isError: boolean;
    isLoading: boolean;
    statusCode: number | null;
    traceId: string | null;
    message: string;
    errors: string[];
}

const StatusSlice = createSlice({
    name: 'status',
    initialState: {
        isSuccess: false,
        isError: false,
        isLoading: true,
        statusCode: null,
        traceId: null,
        message: '',
        errors: [],
    } as StatusState,
    reducers: {
        handleErrorRequest: (state, action) => {
            const payload = action.payload;
            state.isSuccess = false;
            state.isLoading = false;
            state.isError = true;
            state.message = payload.message || 'An error occurred';
            state.errors = payload.errors || [];
            state.statusCode = payload.statusCode || null;
            state.traceId = payload.traceId || null;
        },
        handleResetStatus: (state) => {
            state.isSuccess = false;
            state.isError = false;
            state.isLoading = false;
            state.message = '';
            state.errors = [];
            state.traceId = null;
            state.statusCode = null;
        },
        handleSuccessStatus: (state, action) => {
            state.isSuccess = true;
            state.isError = false;
            state.isLoading = false;
            state.message = action.payload;
        },
    },
    extraReducers: (builder) =>
        builder
            .addCase(authActions.login.fulfilled, (state, action) => {
                const response = action.payload;

                ((state.isSuccess = true),
                    (state.isError = false),
                    (state.isLoading = false),
                    (state.message = response.message));
            })
            .addCase(authActions.login.pending, (state) => {
                ((state.isSuccess = false), (state.isError = false), (state.isLoading = true));
            })
            .addCase(authActions.getCurrentUser.fulfilled, (state, action) => {
                const response = action.payload;

                ((state.isSuccess = true),
                    (state.isError = false),
                    (state.isLoading = false),
                    (state.message = response.message));
            })
            .addCase(authActions.getCurrentUser.pending, (state) => {
                ((state.isSuccess = false), (state.isError = false), (state.isLoading = true), (state.message = ''));
            }),
});

export default StatusSlice.reducer;
export const { handleErrorRequest, handleResetStatus, handleSuccessStatus } = StatusSlice.actions;
