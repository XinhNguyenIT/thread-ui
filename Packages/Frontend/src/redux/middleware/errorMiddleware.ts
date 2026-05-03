import { isRejectedWithValue, Middleware } from '@reduxjs/toolkit';
import { handleErrorRequest } from '@/redux/slices/statusSlice';

// src/redux/middleware/errorMiddleware.ts
export const errorLoggerMiddleware: Middleware = (store) => (next) => (action) => {
    if (isRejectedWithValue(action)) {
        store.dispatch(handleErrorRequest(action.payload));
    }
    return next(action);
};
