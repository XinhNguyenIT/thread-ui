import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import DefaultLayout from '@/layouts/DefaultLayout'; // Trỏ vào index.tsx của layouts
import { publicRoutes, privateRoutes } from './routes';

export default function App() {
    return (
        <BrowserRouter>
            <Routes>
                {/* Render public routes (Login, Register) */}
                {publicRoutes.map((route, index) => {
                    const Page = route.component;
                    return <Route key={index} path={route.path} element={<Page />} />;
                })}

                {/* Render private routes bọc trong DefaultLayout */}
                <Route element={<DefaultLayout />}>
                    {privateRoutes.map((route, index) => {
                        const Page = route.component;
                        return <Route key={index} path={route.path} element={<Page />} />;
                    })}
                </Route>

                <Route path="/" element={<Navigate to="/home" replace />} />
            </Routes>
        </BrowserRouter>
    );
}
