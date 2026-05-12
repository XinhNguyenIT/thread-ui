import React, { useState } from 'react';
import Avatar from '@/components/Avatar';
import { Send, Loader2 } from 'lucide-react';
import { useAppSelector } from '@/hooks/useAppSelector';
import { useComment } from '@/hooks/useComment';
import { formatPostTime } from '@/utils/timeFormat';

interface CommentSectionProps {
    postId: number;
}

const CommentSection = ({ postId }: CommentSectionProps) => {
    const [commentText, setCommentText] = useState('');
    const [isSubmitting, setIsSubmitting] = useState(false);
    const user = useAppSelector((state) => state.auth.information);
    
    // Sử dụng hook custom
    const { comments, sendComment, isLoading } = useComment(postId);

    const handleSendComment = async () => {
        if (!commentText.trim() || isSubmitting) return;

        setIsSubmitting(true);
        const success = await sendComment(commentText);
        if (success) {
            setCommentText('');
        } else {
            alert("Không thể gửi bình luận.");
        }
        setIsSubmitting(false);
    };

    return (
        <div className="mt-2 bg-zinc-50/50 rounded-xl border border-zinc-100 overflow-hidden flex flex-col">
            {/* Khung hiển thị danh sách comment */}
            <div className="max-h-[300px] overflow-y-auto p-3 space-y-4 scrollbar-thin">
                {isLoading ? (
                    <div className="flex justify-center py-4">
                        <Loader2 className="animate-spin text-zinc-400" size={20} />
                    </div>
                ) : comments.length > 0 ? (
                    comments.map((cmt) => (
                        <div key={cmt.id} className="flex gap-3 animate-in fade-in slide-in-from-bottom-2 duration-300">
                            <Avatar src={cmt.author?.avatarSrc} size="sm" />
                            <div className="flex flex-col bg-white p-2 px-3 rounded-2xl shadow-sm border border-zinc-100 max-w-[85%]">
                                <span className="font-bold text-[13px]">
                                    {cmt.author?.lastName} {cmt.author?.firstName}
                                </span>
                                <p className="text-[14px] text-zinc-800 leading-snug">{cmt.content}</p>
                                <span className="text-[11px] text-zinc-400 mt-1">
                                    {cmt.createAt ? formatPostTime(cmt.createAt) : 'Vừa xong'}
                                </span>
                            </div>
                        </div>
                    ))
                ) : (
                    <p className="text-center text-zinc-400 text-[13px] py-4">Chưa có bình luận nào.</p>
                )}
            </div>

            {/* Ô nhập bình luận */}
            <div className="p-3 bg-white border-t border-zinc-100 flex items-center gap-3">
                <Avatar src={user?.avatarSrc} size="sm" />
                <div className="relative flex-1">
                    <input
                        type="text"
                        value={commentText}
                        onChange={(e) => setCommentText(e.target.value)}
                        placeholder="Viết câu trả lời..."
                        disabled={isSubmitting}
                        className="w-full bg-zinc-100 py-2 pl-4 pr-10 rounded-full text-[14px] outline-none focus:ring-1 focus:ring-zinc-200 transition-all disabled:opacity-50"
                        onKeyDown={(e) => e.key === 'Enter' && handleSendComment()}
                    />
                    <button 
                        onClick={handleSendComment}
                        disabled={!commentText.trim() || isSubmitting}
                        className={`absolute right-2 top-1/2 -translate-y-1/2 p-1.5 rounded-full transition-colors 
                            ${commentText.trim() && !isSubmitting ? 'text-black hover:bg-zinc-200' : 'text-zinc-300'}`}
                    >
                        {isSubmitting ? <Loader2 size={16} className="animate-spin" /> : <Send size={16} />}
                    </button>
                </div>
            </div>
        </div>
    );
};

export default CommentSection;