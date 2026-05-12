import { GenderTypeEnum } from '@/common/genderTypeEnum';
import { PrivacyTypeEnum } from '@/common/privacyTypeEnum';
import Posts from './Posts';
import CreatePostForm from '../Forms/CreatePostForm';

interface PostListProps {
    type: 'recommend' | 'following' | 'ghost';
}

const PostList = ({ type }: PostListProps) => {
    const dummyPosts = [
        {
            postId: 1,
            author: {
                firstName: 'Xinh',
                lastName: 'Nguyen',
                gender: GenderTypeEnum.FEMALE, 
                avatarSrc: '',
            },
            caption: 'Đang hoàn thiện component PostList cho dự án InteractHub! 🚀',
            createAt: '2026-05-10T14:37:00', 
            likesCount: 1200, 
            commentsCount: 45,
            medias: [],
            isAvatar: false,
            privacySetting: PrivacyTypeEnum.PUBLIC
        },
        {
            postId: 2,
            author: {
                firstName: 'VN',
                lastName: 'Threads',
                gender: GenderTypeEnum.OTHER,
                avatarSrc: '',
            },
            caption: 'Bạn thích giao diện mới của chúng mình chứ? Đây là 3 tấm ảnh demo lướt ngang nhé!',
            createAt: '2026-05-10T11:37:00', 
            likesCount: 890,
            commentsCount: 12,
            medias: [
                { id: 'm1', src: 'https://images.unsplash.com/photo-1611162617474-5b21e879e113', type: 'image' },
                { id: 'm2', src: 'https://images.unsplash.com/photo-1506744038136-46273834b3fb', type: 'image' },
                { id: 'm3', src: 'https://images.unsplash.com/photo-1501785888041-af3ef285b470', type: 'image' },
                { id: 'm4', src: 'https://images.unsplash.com/photo-1501785888041-af3ef285b470', type: 'image' },
            ],
            isAvatar: false,
            privacySetting: PrivacyTypeEnum.PUBLIC
        },
        {
            postId: 3,
            author: {
                firstName: 'VN',
                lastName: 'Threads',
                gender: GenderTypeEnum.OTHER,
                avatarSrc: '',
            },
            caption: 'Bạn thích giao diện mới của chúng mình chứ? Đây là 3 tấm ảnh demo lướt ngang nhé!',
            createAt: '2026-05-10T11:37:00',
            likesCount: 890,
            commentsCount: 12,
            medias: [
                { id: 'm5', src: 'https://images.unsplash.com/photo-1501785888041-af3ef285b470', type: 'image' }
            ],
            isAvatar: false,
            privacySetting: PrivacyTypeEnum.PUBLIC
        }
    ];

    return (
        <div className="w-full max-w-175 mx-auto bg-white">
            {/* Render các post */}
            {type == 'recommend' && (
                <>
                    <CreatePostForm />
                    <Posts />
                </>
                
            )}

            {type === 'following' && (
                <div className="py-20 text-center text-zinc-400">
                    <p className="italic">Chưa theo dõi ai</p>
                </div>
            )}

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
