import React, { useState } from 'react';
import Avatar from '@/components/Avatar';
import { Send } from 'lucide-react';
import { useAppSelector } from '@/hooks/useAppSelector';

interface CommentSectionProps {
    postId: number;
}

const CommentSection = ({ postId }: CommentSectionProps) => {
    const [commentText, setCommentText] = useState('');
    const user = useAppSelector((state) => state.auth.information);

    const mockComments = [
        { id: 1, author: "Nguyễn Văn A", text: "Bài viết hay quá!", time: "2 phút trước" },
        { id: 2, author: "Trần Thị B", text: "Đỉnh cao luôn bạn ơi 🚀", time: "5 phút trước" },
        { id: 3, author: "Lê Văn C", text: "Cho mình xin info bộ techstack với ạ.", time: "10 phút trước" },
        { id: 4, author: "Lê Văn C", text: "Cho mình xin info bộ techstack với ạ.", time: "10 phút trước" },
        { id: 5, author: "Lê Văn C", text: "Cho mình xin info bộ techstack với ạ.", time: "10 phút trước" },
    ];

    const handleSendComment = () => {
        if (!commentText.trim()) return;
        console.log(`Gửi comment cho bài post ${postId}:`, commentText);
        setCommentText('');
    };

    return (
        <div className="mt-2 bg-zinc-50/50 rounded-xl border border-zinc-100 overflow-hidden flex flex-col">

            <div className="max-h-[300px] overflow-y-auto p-3 space-y-4 scrollbar-thin">
                {mockComments.map((cmt) => (
                    <div key={cmt.id} className="flex gap-3">
                        <Avatar size="sm" />
                        <div className="flex flex-col bg-white p-2 px-3 rounded-2xl shadow-sm border border-zinc-100 max-w-[85%]">
                            <span className="font-bold text-[13px]">{cmt.author}</span>
                            <p className="text-[14px] text-zinc-800">{cmt.text}</p>
                            <span className="text-[11px] text-zinc-400 mt-1">{cmt.time}</span>
                        </div>
                    </div>
                ))}
            </div>

            <div className="p-3 bg-white border-t border-zinc-100 flex items-center gap-3">
                <Avatar src={user?.avatarSrc} size="sm" />
                <div className="relative flex-1">
                    <input
                        type="text"
                        value={commentText}
                        onChange={(e) => setCommentText(e.target.value)}
                        placeholder="Viết câu trả lời..."
                        className="w-full bg-zinc-100 py-2 pl-4 pr-10 rounded-full text-[14px] outline-none focus:ring-1 focus:ring-zinc-200 transition-all"
                        onKeyDown={(e) => e.key === 'Enter' && handleSendComment()}
                    />
                    <button 
                        onClick={handleSendComment}
                        className={`absolute right-2 top-1/2 -translate-y-1/2 p-1.5 rounded-full transition-colors ${commentText.trim() ? 'text-black hover:bg-zinc-200' : 'text-zinc-300'}`}
                    >
                        <Send size={16} />
                    </button>
                </div>
            </div>
        </div>
    );
};

export default CommentSection;