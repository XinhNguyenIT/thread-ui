import React, { useState } from 'react';
import { Heart } from 'lucide-react';
import ActionButton from '@/components/Button/ActionButton';
import { interactLikeAction } from '@/api/like/likeService';

interface LikeButtonProps {
    postId: number;
    initialLikesCount: number;
    initialIsLiked: boolean;
}

const LikeButton = ({ postId, initialLikesCount, initialIsLiked }: LikeButtonProps) => {
    const [isLiked, setIsLiked] = useState(initialIsLiked);
    const [count, setCount] = useState(initialLikesCount);
    const [isLoading, setIsLoading] = useState(false);
    // console.log("post id",postId)

    const handleLike = async () => {
        if (isLoading) return; // Tránh spam click

        // Optimistic UI: Cập nhật giao diện trước khi API phản hồi để tạo cảm giác mượt mà
        const prevLiked = isLiked;
        setIsLiked(!prevLiked);
        setCount(prev => prevLiked ? prev - 1 : prev + 1);
        setIsLoading(true);

        //Dung roi no co vay thoi them enum targetType laf POST,STORY,COMMENT roi moi cai quang no cai action vao la dc
        // oke quen cai nay, de t thu lai cai
        try {
            await interactLikeAction({
                targetId: postId,
                targetType: "POST"
            });
            // Nếu API thành công, không cần làm gì thêm vì UI đã update ở trên
        } catch (error) {
            // Nếu lỗi, hoàn tác (rollback) trạng thái giao diện
            setIsLiked(prevLiked);
            setCount(prev => prevLiked ? prev + 1 : prev - 1);
            console.error("Like failed:", error);
        } finally {
            setIsLoading(false);
        }
    };

    return (
        <ActionButton
            icon={
                <Heart 
                    size={20} 
                    className={`transition-colors ${isLiked ? "fill-red-500 text-red-500" : "text-zinc-500"}`} 
                />
            }
            count={count}
            ariaLabel="Like"
            onClick={handleLike}
        />
    );
};

export default LikeButton;