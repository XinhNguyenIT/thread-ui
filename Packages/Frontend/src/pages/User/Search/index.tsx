import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Search as SearchIcon, SlidersHorizontal, ArrowLeft, MoreHorizontal } from 'lucide-react';
import { trendingData, PostData } from './posts';

const SearchPage: React.FC = () => {
    const navigate = useNavigate();
    const [searchValue, setSearchValue] = useState('');

    return (
        <main className="flex-1 w-full flex justify-center py-1 h-full bg-[#fbfbfb]">
            {/* Khung bao xung quanh giống trang Home */}
            <div className="w-[800px] h-full border border-zinc-100 rounded-[32px] bg-white shadow-sm flex flex-col overflow-hidden">
                {/* Header bên trong khung */}

                {/* Nội dung cuộn bên trong khung */}
                <div className="flex-1 overflow-y-auto p-6 scrollbar-hide">
                    <div className="max-w-[620px] mx-auto w-full">
                        {/* Search Bar */}
                        <div className="relative mb-6">
                            <SearchIcon className="absolute left-4 top-1/2 -translate-y-1/2 text-zinc-400" size={18} />
                            <input
                                type="text"
                                value={searchValue}
                                onChange={(e) => setSearchValue(e.target.value)}
                                placeholder="Search"
                                className="w-full h-12 pl-12 pr-12 bg-zinc-100 rounded-2xl outline-none focus:bg-white focus:border focus:border-zinc-200 transition-all"
                            />
                            <SlidersHorizontal
                                className="absolute right-4 top-1/2 -translate-y-1/2 text-zinc-400 cursor-pointer"
                                size={18}
                            />
                        </div>

                        {/* Trending Label */}
                        <div className="mb-4">
                            <h2 className="text-xl font-bold">
                                <span className="bg-[#facc15] px-2 py-0.5 rounded-md italic transform -rotate-1 inline-block">
                                    Trending now
                                </span>
                            </h2>
                        </div>

                        {/* List Posts */}
                        <div className="divide-y divide-zinc-50">
                            {trendingData.map((post: PostData) => (
                                <div
                                    key={post.id}
                                    onClick={() => navigate(`/post-detail/${post.id}`)}
                                    className="flex items-start justify-between py-5 cursor-pointer hover:bg-zinc-50/50 transition-all rounded-xl px-2"
                                >
                                    <div className="flex-1 pr-4">
                                        <h3 className="font-bold text-[16px] text-zinc-900 leading-tight group-hover:underline">
                                            {post.title}
                                        </h3>
                                        <p className="text-[14px] text-zinc-500 line-clamp-1 mt-1 font-light">
                                            {post.desc}
                                        </p>
                                        <span className="text-[12px] text-zinc-400 font-medium uppercase mt-2 block">
                                            {post.posts}
                                        </span>
                                    </div>
                                    <img
                                        src={post.image}
                                        className="w-[72px] h-[72px] rounded-2xl object-cover border border-zinc-100 shrink-0"
                                        alt={post.title}
                                    />
                                </div>
                            ))}
                        </div>
                    </div>
                </div>
            </div>
        </main>
    );
};

export default SearchPage;
