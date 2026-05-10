// call API for posts data
import { getPost } from '@/api/post/fileService';
import { PostProps } from '@/components/PostList/Post';
import { useState, useEffect } from 'react';

export const usePosts = (page: number, pageSize: number) => {
    const [posts, setPosts] = useState<PostProps[]>([]);

    const fetchPosts = async () => {
        try {
            const data = await getPost({page, pageSize });
            console.log("data posts:", data)
            setPosts(data.data);
        } catch (err) {
            console.error("Post call error:", err);
        }
    };

    // Tự động gọi lại nếu page hoặc pageSize thay đổi
    useEffect(() => {
        fetchPosts();
    }, [page, pageSize]); 

    return { posts, refresh: fetchPosts };
};