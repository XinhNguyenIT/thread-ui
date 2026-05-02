import { post } from '@/utils/httpRequest';

const routes = {
    register: '/register',
    login: '/login', // Sửa 'Login' thành 'login'
    home: '/home',
    following: '/following',
    search: '/search',
    activity: '/activity',
    profile: '/profile',
    postDetail: '/post/:id', // Thêm route cho chi tiết bài viết
};

export default routes;
