import Post from "@/components/PostList/Post";
import { usePosts } from "@/hooks/usePost"; 
import InfiniteScrollDetector from "./InfiniteScrollDetector";
import { useAppSelector } from "@/hooks/useAppSelector"; // Import selector
import { useAuth } from "@/hooks/useAuth";

interface PostsProps {
    pageSize?: number;
}

const UserPosts = ({ pageSize = 5 }: PostsProps) => {
    const { posts, loadMore, hasMore } = usePosts(pageSize);
    
    // Lấy thông tin user hiện tại từ store
    const userFromStore = useAuth()
    // const userFromStore = useAppSelector((state) => state.auth.information);
    console.log("user from store: ", userFromStore)

    // const filteredPosts = posts.filter(post => post.author?.userId === userFromStore?.id);
    const filteredPosts = posts.filter(post => post.author?.userId === '88888888-8888-8888-8888-888888888888');
    console.log("FilteredPosts: ", filteredPosts)

    if (!filteredPosts || filteredPosts.length === 0 && !hasMore) {
        return (
            <div className="py-16 text-center">
                <p className="m-auto text-neutral-500">Chưa có thread nào của bạn</p>
            </div>
        );
    }

    return (
        <div className="flex flex-col">
            {/* Render danh sách đã lọc */}
            {filteredPosts.map((post) => (
                <Post key={post.postId} {...post} />
            ))}

            {/* 
               Lưu ý: loadMore vẫn sẽ gọi API lấy thêm bài viết mới (có thể của người khác), 
               sau đó filter sẽ tiếp tục lọc lại ở lần render kế tiếp.
            */}
            <InfiniteScrollDetector 
                onIntersect={loadMore} 
                hasMore={hasMore} 
            />
        </div>
    );
};

export default UserPosts;