import React from 'react';

interface HeaderProps {
    type: 'home' | 'other';
    title?: string;
    activeTab?: string;
    onTabChange?: (tab: string) => void;
}

const Header = ({ type, title, activeTab, onTabChange }: HeaderProps) => {
    return (
        <header className="sticky top-0 z-30 w-full bg-white/80 backdrop-blur-md border-b border-zinc-100">
            <div className="max-w-[640px] mx-auto h-[60px] flex items-center justify-center relative">
                {type === 'home' ? (
                    // Header 3 cột cho trang Home
                    <div className="flex w-full justify-around text-[15px] font-bold">
                        {['For you', 'Following', 'Ghost posts'].map((tab) => (
                            <button
                                key={tab}
                                onClick={() => onTabChange?.(tab)}
                                className={`px-4 py-2 transition-colors ${
                                    activeTab === tab ? 'text-black' : 'text-zinc-400'
                                }`}
                            >
                                {tab}
                            </button>
                        ))}
                    </div>
                ) : (
                    // Header 1 cột cho các trang khác
                    <h2 className="text-[16px] font-bold text-black">{title}</h2>
                )}
            </div>
        </header>
    );
};
export default Header;
