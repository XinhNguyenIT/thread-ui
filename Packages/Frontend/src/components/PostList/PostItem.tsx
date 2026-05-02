import React from 'react';
import Avatar from '@/components/Avatar';
import ActionButton from '@/components/Button/ActionButton';
import Image from '@/components/Image';
import { Heart, MessageCircle, Repeat2, Send, MoreHorizontal } from 'lucide-react';

interface PostItemProps {
    authorName: string;
    username: string;
    content: string;
    timestamp: string;
    avatarUrl?: string;
    // SỬA TẠI ĐÂY: Đổi postImage thành postImages (mảng các chuỗi)
    postImages?: string[];
    likes: number | string;
    replies: number | string;
}

const PostItem = ({
    authorName,
    username,
    content,
    timestamp,
    avatarUrl,
    // SỬA TẠI ĐÂY: Nhận đúng tên biến postImages
    postImages,
    likes,
    replies,
}: PostItemProps) => {
    return (
        <div className="flex gap-3 p-4 border-b border-zinc-100 hover:bg-zinc-50/30 transition-colors">
            {/* Cột trái: Avatar */}
            <div className="flex flex-col items-center gap-2">
                <Avatar src={avatarUrl} size="lg" />
                <div className="w-[2px] flex-1 bg-zinc-100 rounded-full my-1"></div>
            </div>

            {/* Cột phải: Nội dung bài viết */}
            <div className="flex-1 flex flex-col gap-1">
                <div className="flex justify-between items-center">
                    <div className="flex items-center gap-2">
                        <span className="font-bold text-[15px] hover:underline cursor-pointer">{username}</span>
                        <span className="text-zinc-400 text-[14px]">{timestamp}</span>
                    </div>
                    <button className="text-zinc-400 hover:text-black">
                        <MoreHorizontal size={18} />
                    </button>
                </div>

                <p className="text-[15px] text-[#1c1e21] leading-relaxed whitespace-pre-wrap">{content}</p>

                {postImages && postImages.length > 0 && (
                    <div className="mt-3 w-full scrollbar-hide">
                        {/* - flex: Cho phép các ảnh nằm ngang
          - overflow-x-auto: Nếu tổng ảnh quá rộng sẽ cho lướt ngang
          - gap-1: Khoảng cách giữa các ảnh là 4px
        */}
                        <div className="flex gap-1 overflow-x-auto flex-nowrap scrollbar-hide snap-x snap-mandatory ">
                            {postImages.map((img, index) => {
                                // Logic tính toán độ rộng:
                                // Nếu chỉ có 1 ảnh: w-full (100%)
                                // Nếu có nhiều ảnh: w-[48%] hoặc w-[32%] để hiện dạng chia cột
                                const itemWidth =
                                    postImages.length === 1
                                        ? 'w-full h-[400px]'
                                        : postImages.length === 2
                                          ? 'w-[49%]'
                                          : 'w-[32.5%]';

                                return (
                                    <div
                                        key={index}
                                        className={`flex-shrink-0 ${itemWidth} aspect-[4/5] rounded-xl overflow-hidden border border-zinc-200 snap-start`}
                                    >
                                        <Image
                                            src={img}
                                            alt={`Post content ${index}`}
                                            className="w-full h-full object-cover"
                                        />
                                    </div>
                                );
                            })}
                        </div>
                    </div>
                )}

                {/* Hàng nút tương tác - Đã chỉnh icon đều nhau size 18 cho dễ nhìn */}
                <div className="flex gap-4 mt-3 -ml-2">
                    <ActionButton icon={<Heart size={12} />} count={likes} ariaLabel="Like" />
                    <ActionButton icon={<MessageCircle size={12} />} count={replies} ariaLabel="Reply" />
                    <ActionButton icon={<Repeat2 size={12} />} ariaLabel="Repost" />
                    <ActionButton icon={<Send size={12} />} ariaLabel="Share" />
                </div>
            </div>
        </div>
    );
};

export default PostItem;
