import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import { publicRoutes, privateRoutes } from './routes';
import PublicRoute from './components/Routes/PublicRoute';
import PrivateRoute from './components/Routes/PrivateRoute';
import { authActions } from './redux/actions/authActions';
import { useAppDispatch } from './hooks/useAppDispatch';
import { useAppSelector } from './hooks/useAppSelector';
import { useEffect, useState } from 'react';
import Spinner from './components/Spinner';
import DefaultLayout from './layouts/DefaultLayout';
import ErrorDialog from './components/Dialog/ErrorDialog';
import { handleResetStatus, handleSuccessStatus } from './redux/slices/statusSlice';
import { getPost } from './api/post/fileService';

export default function App() {
    const dispatch = useAppDispatch();
    const [loading, setLoading] = useState(true);

    const status = useAppSelector((state) => state.status);
    const { isError, isLoading, ...statusToShow } = status;

    const { information } = useAppSelector((state) => state.auth);

    useEffect(() => {
        const getUser = async () => {
            try {
                await dispatch(authActions.getCurrentUser());
            } catch (error) {
                console.log('Lỗi', error);
            } finally {
                setLoading(false);
            }
        };
        !information && getUser();
    }, []);



    if (loading) return <Spinner />;
    return (
        <>
            <ErrorDialog
                isOpen={status.isError}
                errorData={statusToShow}
                onClose={() => dispatch(handleResetStatus())}
            />
            <BrowserRouter
                future={{
                    v7_startTransition: true,
                    v7_relativeSplatPath: true,
                }}
            >
                <Routes>
                    {publicRoutes.map((route, index) => (
                        <Route key={index} path={route.path} element={<PublicRoute>{route.component}</PublicRoute>} />
                    ))}

                    {privateRoutes.map((route, index) => (
                        <Route element={<DefaultLayout />}>
                            <Route
                                key={index}
                                path={route.path}
                                element={<PrivateRoute>{route.component}</PrivateRoute>}
                            />
                        </Route>
                    ))}
                </Routes>
            </BrowserRouter>
        </>
    );
}
