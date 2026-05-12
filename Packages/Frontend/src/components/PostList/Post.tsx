import Avatar from '@/components/Avatar';
import ActionButton from '@/components/Button/ActionButton';
import Image from '@/components/Image';
import { Heart, MessageCircle, Repeat2, Send, MoreHorizontal, Trash2, Edit3, UserMinus, Flag } from 'lucide-react';
import { GenderTypeEnum } from "@/common/genderTypeEnum"
import { PrivacyTypeEnum } from "@/common/privacyTypeEnum"
import { formatPostTime } from '@/utils/timeFormat';
import LikeButton from './LikeButton';
import { useEffect, useRef, useState } from 'react';
import CommentSection from './CommentSection';
import { deletePost } from '@/api/post/fileService';

export interface PostProps {
    author?: {
        userId?: string
        firstName?: string
        lastName?: string
        gender?: GenderTypeEnum
        avatarSrc?: string
    }
    caption?: string
    commentsCount?: number
    createAt?: string
    isAvatar?: boolean
    likesCount?: number
    medias?: {
        id?: string
        src?: string
        type?: string
    }[]
    postId?: number
    privacySetting?: PrivacyTypeEnum
    // test
    isLiked?: boolean
}

const Post = ({
    author = {
        gender: GenderTypeEnum.UNKNOWN
    },
    caption,
    commentsCount = 0,
    createAt,
    isAvatar = false,
    likesCount = 0,
    medias = [],
    postId,
    privacySetting = PrivacyTypeEnum.PRIVATE,
    // test
    isLiked = false
}: PostProps) => {

    const [showComments, setShowComments] = useState(false);
    const [isMenuOpen, setIsMenuOpen] = useState(false);
    const menuRef = useRef<HTMLDivElement>(null);

    useEffect(() => {
        const handleClickOutside = (event: MouseEvent) => {
            if (menuRef.current && !menuRef.current.contains(event.target as Node)) {
                setIsMenuOpen(false);
            }
        };
        document.addEventListener('mousedown', handleClickOutside);
        return () => document.removeEventListener('mousedown', handleClickOutside);
    }, []);

    const handleDeletePost = async () => {
        // Nên có confirm để tránh bấm nhầm
        const isConfirm = window.confirm("Bạn có chắc chắn muốn xóa bài viết này không?");
        if (!isConfirm) return;

        console.log("Post id", postId)
        try {
            await deletePost(postId!); 
        
            alert("Xóa bài viết thành công!");
            window.location.reload(); 
        } catch (error) {
            console.error("Lỗi khi xóa post:", error);
            alert("Xóa thất bại, vui lòng thử lại.");
        } finally {
            setIsMenuOpen(false); // Đóng menu lại
        }
    };

    return (
        <div className="flex gap-3 p-4 border-b border-zinc-100 hover:bg-zinc-50/30 transition-colors">
            {/* Cột trái: Avatar */}
            <div className="flex flex-col items-center gap-2">
                <Avatar src={author?.avatarSrc} size="lg" />
                <div className="w-0.5 flex-1 bg-zinc-100 rounded-full my-1"></div>
            </div>

            {/* Cột phải: Nội dung bài viết */}
            <div className="flex-1 flex flex-col gap-1">
                <div className="flex justify-between items-center">
                    <div className="flex items-center gap-2">
                        <span className="font-bold text-[15px] hover:underline cursor-pointer">{author?.lastName} {author?.firstName}</span>
                        <span className="text-zinc-400 text-[14px]">{createAt ? formatPostTime(createAt) : ''}</span>
                    </div>
                    
                    <div className="relative" ref={menuRef}>
                        <button 
                            className={`p-2 rounded-full transition-colors ${isMenuOpen ? 'bg-zinc-100 text-black' : 'text-zinc-400 hover:text-black hover:bg-zinc-100'}`}
                            onClick={() => setIsMenuOpen(!isMenuOpen)}
                        >
                            <MoreHorizontal size={18} />
                        </button>

                        {/* Dropdown Menu */}
                        {isMenuOpen && (
                            <div className="absolute right-0 mt-1 w-56 bg-white border border-zinc-100 rounded-xl shadow-xl z-50 py-2 animate-in fade-in zoom-in duration-150">
                                <button onClick={handleDeletePost} className="w-full px-4 py-2.5 text-left text-[14px] font-semibold flex items-center gap-3 hover:bg-zinc-50 transition-colors text-red-600">
                                    <Trash2 size={18} />
                                    Xóa bài viết
                                </button>
                            </div>
                        )}
                    </div>
                </div>

                <p className="text-[15px] text-[#1c1e21] leading-relaxed whitespace-pre-wrap">{caption}</p>

                {medias && medias.length > 0 && (
                    <div className="mt-3 w-full scrollbar-hide">
                        <div className="flex gap-1 overflow-x-auto flex-nowrap scrollbar-hide snap-x snap-mandatory ">
                            {medias.map((img, index) => {
                                // Nếu chỉ có 1 ảnh: w-full (100%)
                                // Nếu có nhiều ảnh: w-[48%] hoặc w-[32%] để hiện dạng chia cột
                                const itemWidth =
                                    medias.length === 1
                                        ? 'w-full h-[400px]'
                                        : medias.length === 2
                                          ? 'w-[49%]'
                                          : 'w-[32.5%]';

                                return (
                                    <div key={index} className={`shrink-0 ${itemWidth} aspect-4/5 rounded-xl overflow-hidden border border-zinc-200 snap-start`}>
                                        {img.src && 
                                            <Image
                                                src={img.src}
                                                alt={`Post content ${index}`}
                                                className="w-full h-full object-cover"
                                            />
                                        }
                                        
                                    </div>
                                );
                            })}
                        </div>
                    </div>
                )}

                <div className="flex gap-4 mt-3 -ml-2">
                    {postId && (
                        <LikeButton
                            postId={postId} 
                            initialLikesCount={likesCount} 
                            initialIsLiked={isLiked} 
                        />
                    )}
                    {/* <ActionButton icon={<MessageCircle size={12} />} count={commentsCount} ariaLabel="Reply" /> */}
                    {/* <ActionButton icon={<Repeat2 size={12} />} ariaLabel="Repost" />
                    <ActionButton icon={<Send size={12} />} ariaLabel="Share" /> */}

                    <ActionButton 
                        icon={<MessageCircle size={12} />} 
                        count={commentsCount} 
                        ariaLabel="Reply" 
                        onClick={() => setShowComments(!showComments)} // Thêm cái này
                    />
                </div>

                {showComments && postId && (
                    <CommentSection postId={postId} />
                )}
            </div>
        </div>
    );
};

export default Post;
