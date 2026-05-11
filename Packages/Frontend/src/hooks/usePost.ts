// call API for posts data
import { getPost } from '@/api/post/fileService';
import { PostProps } from '@/components/PostList/Post';
import { useState, useEffect } from 'react';


export const usePosts = (pageSize: number) => {
    // Mảng lưu post
    const [posts, setPosts] = useState<PostProps[]>([]);

    // số THỨ TỰ trang hiện tại, cộng dồn lên mỗi lần loadmore
    const [page, setPage] = useState(1);
    const [hasMore, setHasMore] = useState(true);
    const [isLoading, setIsLoading] = useState(false);

    const fetchPosts = async (targetPage: number) => {
        if (isLoading) return;
        setIsLoading(true);
        try {
            const data = await getPost({ page: targetPage, pageSize });
            const newPosts = data.data;

            // số lượng bài nhận về < pageSize (4)
            if (newPosts.length < pageSize) {
                setHasMore(false); // Hết dữ liệu để load
            }

            setPosts(prev => targetPage === 1 ? newPosts : [...prev, ...newPosts]);
        } catch (err) {
            console.error("Post call error:", err);
        } finally {
            setIsLoading(false);
        }
    };

    useEffect(() => {
        fetchPosts(page);
    }, [page]);

    const loadMore = () => {
        if (hasMore && !isLoading) {
            setPage(prev => prev + 1);
        }
    };

    return { posts, loadMore, hasMore, isLoading };
};