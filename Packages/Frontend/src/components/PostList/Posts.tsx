import Post from "@/components/PostList/Post";
import { usePosts } from "@/hooks/usePost"; // Đường dẫn tới hook của bạn
import InfiniteScrollDetector from "./InfiniteScrollDetector";

interface PostsProps {
    pageSize?: number;
}

const Posts = ({ pageSize = 5 }: PostsProps) => {
    const { posts, loadMore, hasMore } = usePosts(pageSize);

    if (!posts || posts.length === 0 && !hasMore) {
        return (
            <div className="py-16 text-center">
                <p className="m-auto text-neutral-500">Chưa có thread nào</p>
            </div>
        );
    }

    return (
        <div className="flex flex-col">
            {posts.map((post) => (
                <Post key={post.postId} {...post} />
            ))}

            {/* Bộ cảm biến để load thêm trang - post mới */}
            <InfiniteScrollDetector 
                onIntersect={loadMore} 
                hasMore={hasMore} 
            />
        </div>
    );
};

export default Posts;