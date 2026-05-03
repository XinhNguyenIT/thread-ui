import { Navigate } from 'react-router-dom';
import { useSelector } from 'react-redux';
import { ReactNode } from 'react';
import { useAppSelector } from '@/hooks/useAppSelector';

interface PublicRouteProps {
    children: ReactNode;
}

export default function PublicRoute({ children }: PublicRouteProps) {
    const { isAuthenticated } = useAppSelector((state) => state.auth);
    return isAuthenticated ? <Navigate to="/" replace /> : children;
}
