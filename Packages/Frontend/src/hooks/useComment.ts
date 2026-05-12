import { useState, useEffect } from 'react';
import { getComment, postNewComment } from '@/api/comment/commentService';

export const useComment = (postId: number) => {
    const [comments, setComments] = useState<any[]>([]);
    const [isLoading, setIsLoading] = useState(false);

    const fetchComments = async () => {
        setIsLoading(true);
        try {
            const response = await getComment({ postId });
            if (response.success) {
                setComments(response.data);
            }
        } catch (error) {
            console.error("Lấy comment thất bại:", error);
        } finally {
            setIsLoading(false);
        }
    };

    const sendComment = async (content: string) => {
        try {
            const payload = {
                postId: postId,
                content: content,
                commentId: 0 // 0 nếu là comment trực tiếp vào bài post
            };
            const response = await postNewComment(payload);
            
            if (response.success) {
                // Thay vì reload trang, ta thêm trực tiếp vào danh sách để UX mượt hơn
                // Hoặc gọi lại fetchComments()
                fetchComments();
                return true;
            }
        } catch (error) {
            console.error("Gửi comment thất bại:", error);
            return false;
        }
    };

    useEffect(() => {
        if (postId) fetchComments();
    }, [postId]);

    return { comments, sendComment, isLoading, refresh: fetchComments };
};