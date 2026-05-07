import config from '@/config';
import HomePage from '@/pages/User/Home';
import Search from '@/pages/User/Search';
import Activity from '@/pages/User/Activity';
import Profile from '@/pages/User/Profile';
import Login from '@/pages/User/Login';
import Register from '@/pages/User/Register';
import PostDetail from '@/pages/User/Search/PostDetail';
import Testing from '@/pages/User/Testing';

const publicRoutes = [
    { path: config.routes.login, component: <Login /> },
    { path: config.routes.register, component: <Register /> }, // 2. Khai báo Register vào publicRoutes
];

const privateRoutes = [
    { path: config.routes.home, component: <HomePage /> },
    { path: config.routes.search, component: <Search /> },
    { path: config.routes.activity, component: <Activity /> },
    { path: config.routes.profile, component: <Profile /> },
    { path: config.routes.testing, component: <Testing /> },
    // Trong routes.ts hoặc App.tsx
    { path: '/post-detail/:id', component: <PostDetail /> },
];

export { publicRoutes, privateRoutes };
