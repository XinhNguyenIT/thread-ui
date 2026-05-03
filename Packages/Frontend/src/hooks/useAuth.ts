import { Navigate, Outlet } from 'react-router-dom';
import { useAppSelector } from './useAppSelector';
import { RootState } from '@/redux/store';

export const useAuth = () => {
    const user = useAppSelector((state: RootState) => state.auth.information);

    return {
        // !!user sẽ trả về true nếu user tồn tại, false nếu null/undefined
        isAuthenticated: !!user,
        user: user,
        // Bạn có thể lấy thêm các thông tin khác từ store nếu cần
        role: user?.role || null,
    };
};
