import { Navigate } from 'react-router-dom';
import { ReactNode } from 'react';
import { useAppSelector } from '@/hooks/useAppSelector';

interface PrivateRouteProps {
    children: ReactNode; // Định nghĩa kiểu cho children trong React
}

export default function PrivateRoute({ children }: PrivateRouteProps) {
    const { isAuthenticated } = useAppSelector((state) => state.auth);
    return isAuthenticated ? children : <Navigate to="/login" replace />;
}
