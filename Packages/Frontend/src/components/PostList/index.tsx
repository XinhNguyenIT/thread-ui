import React from 'react';
import PostItem from './PostItem';

interface PostListProps {
    type: 'recommend' | 'following' | 'ghost';
}

const PostList = ({ type }: PostListProps) => {
    // Dữ liệu mẫu đã cập nhật postImages thành mảng 3 ảnh
    const dummyPosts = [
        {
            id: 1,
            authorName: 'Xinh Nguyen',
            username: 'nguyenxinh',
            content: 'Đang hoàn thiện component PostList cho dự án InteractHub! 🚀',
            timestamp: '2h',
            likes: '1.2K',
            replies: 45,
            postImages: [], // Bài này không có ảnh
        },
        {
            id: 2,
            authorName: 'Threads VN',
            username: 'threads_vn',
            content: 'Bạn thích giao diện mới của chúng mình chứ? Đây là 3 tấm ảnh demo lướt ngang nhé!',
            timestamp: '5h',
            // SỬA TẠI ĐÂY: Chuyển từ postImage thành postImages và thêm 3 link ảnh
            postImages: [
                'https://images.unsplash.com/photo-1611162617474-5b21e879e113',
                'https://images.unsplash.com/photo-1506744038136-46273834b3fb',
                'https://images.unsplash.com/photo-1501785888041-af3ef285b470',
                'https://images.unsplash.com/photo-1501785888041-af3ef285b470',
            ],
            likes: 890,
            replies: 12,
        },
        {
            id: 3,
            authorName: 'Threads VN',
            username: 'threads_vn',
            content: 'Bạn thích giao diện mới của chúng mình chứ? Đây là 3 tấm ảnh demo lướt ngang nhé!',
            timestamp: '5h',
            // SỬA TẠI ĐÂY: Chuyển từ postImage thành postImages và thêm 3 link ảnh
            postImages: ['https://images.unsplash.com/photo-1501785888041-af3ef285b470'],
            likes: 890,
            replies: 12,
        },
    ];

    return (
        <div className="w-full max-w-[700px] mx-auto bg-white">
            {dummyPosts.map((post) => (
                <PostItem key={post.id} {...post} />
            ))}

            {/* Hiển thị thông báo khi tab trống */}
            {type === 'ghost' && (
                <div className="py-20 text-center text-zinc-400">
                    <p className="italic">Chưa có bài viết ẩn danh nào...</p>
                </div>
            )}
        </div>
    );
};

export default PostList;
