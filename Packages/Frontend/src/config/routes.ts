import { post } from '@/utils/httpRequest';

const routes = {
    register: '/register',
    login: '/login', // Sửa 'Login' thành 'login'
    home: '/',
    following: '/following',
    search: '/search',
    activity: '/activity',
    profile: '/profile',
    testing: '/testing',
    postDetail: '/post/:id', // Thêm route cho chi tiết bài viết
};

export default routes;
