import React from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { ArrowLeft, MoreHorizontal, Heart, MessageCircle, Repeat2, Send } from 'lucide-react';
// Import cả dữ liệu và Interface
import { trendingData, PostData } from '@/pages/User/Search/posts';

const PostDetail: React.FC = () => {
    const { id } = useParams<{ id: string }>(); // Định nghĩa id là string từ URL
    const navigate = useNavigate();

    // Tìm bài viết và ép kiểu hoặc dùng find với Type
    const post = trendingData.find((p: PostData) => p.id === Number(id));

    if (!post) {
        return <div className="p-10 text-center">Không tìm thấy bài viết</div>;
    }

    return (
        <div className="flex-1 w-full flex flex-col items-center min-h-screen bg-white">
            <header className="w-full max-w-[800px] h-14 flex items-center justify-between px-8 sticky top-0 bg-white z-10">
                <button onClick={() => navigate(-1)} className="p-2 hover:bg-zinc-100 rounded-full">
                    <ArrowLeft size={20} />
                </button>
                <h1 className="text-[16px] font-bold">{post.title}</h1>
                <button className="p-2 hover:bg-zinc-100 rounded-full">
                    <MoreHorizontal size={20} />
                </button>
            </header>

            <div className="w-full max-w-[620px] flex flex-col px-4">
                <div className="my-4 p-6 border border-zinc-100 rounded-[32px] bg-white shadow-sm">
                    <span className="text-[12px] text-zinc-400 block mb-2 font-medium">Summarized by AI</span>
                    <p className="text-[15px] text-zinc-800">{post.desc}</p>
                </div>

                <div className="flex gap-3 py-4">
                    <img src={post.avatar} className="w-9 h-9 rounded-full object-cover" alt="avatar" />
                    <div className="flex-1">
                        <span className="font-bold text-[15px]">{post.username}</span>
                        <p className="text-[15px] my-2">{post.desc}</p>
                        <img src={post.image} className="w-full rounded-2xl border" alt="content" />

                        <div className="flex items-center gap-4 text-zinc-500 mt-4">
                            <Heart size={20} />
                            <MessageCircle size={20} />
                            <Repeat2 size={20} />
                            <Send size={20} />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default PostDetail;
