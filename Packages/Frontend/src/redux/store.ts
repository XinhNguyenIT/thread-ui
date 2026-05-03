import { configureStore } from '@reduxjs/toolkit';
import authReducer from '@/redux/slices/authSlice';
import statusReducer from '@/redux/slices/statusSlice';
import { injectStore } from '../api/axiosInstance';

const store = configureStore({
    reducer: {
        status: statusReducer,
        auth: authReducer,
    },
});

injectStore(store);

export default store;
export type AppStore = typeof store;
export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
