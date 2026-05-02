import React from 'react';
import { useOutletContext } from 'react-router-dom';
import PostList from '@/components/PostList';

const HomePage = () => {
    // Lấy context từ Outlet của DefaultLayout
    const { activeTab } = useOutletContext<{ activeTab: string }>();

    return (
        <main className="flex-1 w-full flex justify-center overflow-hidden py-1 h-full">
            <div className="w-[800px] h-full border border-zinc-100 rounded-[32px] bg-white shadow-sm flex flex-col overflow-hidden">
                <div className="flex-1 overflow-y-auto p-6 scrollbar-hide">
                    {/* Hiển thị bài viết dựa trên tab được nhấn ở Header */}
                    {activeTab === 'For you' && <PostList type="recommend" />}
                    {activeTab === 'Following' && <PostList type="following" />}
                    {activeTab === 'Ghost posts' && <PostList type="ghost" />}
                </div>
            </div>
        </main>
    );
};

export default HomePage;
