import React, { useState, useRef, useEffect } from 'react';
import { Home, Search, Plus, Heart, User, Menu, Settings, Moon, LogOut, MessageSquareWarning } from 'lucide-react';
import ActionButton from '@/components/Button/ActionButton';
import Image from '@/components/Image';
import { logout } from '@/redux/slices/authSlice';
import { useAppDispatch } from '@/hooks/useAppDispatch';
import { authActions } from '@/redux/actions/authActions';

export default function Sidebar() {
    const [isMenuOpen, setIsMenuOpen] = useState(false);
    const menuRef = useRef<HTMLDivElement>(null);
    const dispatch = useAppDispatch();


    const handleLogout = async () => {
        await dispatch(authActions.logout());
    };

    // Xử lý đóng menu khi click ra ngoài vùng menu
    useEffect(() => {
        const handleClickOutside = (event: MouseEvent) => {
            if (menuRef.current && !menuRef.current.contains(event.target as Node)) {
                setIsMenuOpen(false);
            }
        };
        document.addEventListener('mousedown', handleClickOutside);
        return () => document.removeEventListener('mousedown', handleClickOutside);
    }, []);

    return (
        <aside className="fixed left-0 top-0 h-screen w-[80px] flex flex-col items-center py-6 border-r border-zinc-200 bg-white z-50 justify-between">
            {/* 1. Logo */}
            <div className="mb-10 cursor-pointer">
                <Image src="/images/ana1.jpg" alt="Logo" className="w-16 h-16 object-contain" />
            </div>

            {/* 2. Menu chính */}
            <div className="flex-1 flex flex-col gap-8 flex-col h-full lex-1 justify-center">
                <ActionButton icon={<Home />} ariaLabel="Home" to="/" variant="active" />
                <ActionButton icon={<Search />} ariaLabel="Search" to="/search" />
                <ActionButton
                    icon={<Plus className="bg-zinc-100 rounded-lg p-1 w-8 h-8" />}
                    ariaLabel="Create Post"
                    onClick={() => console.log('Open Create Modal')}
                />
                <ActionButton icon={<Heart />} ariaLabel="Activity" to="/activity" />
                <ActionButton icon={<User />} ariaLabel="Profile" to="/profile" />
            </div>

            {/* 3. Nút Menu/Cài đặt dưới cùng với Pop-up */}
            <div className="mt-auto relative" ref={menuRef}>
                {isMenuOpen && (
                    <div className="absolute bottom-full left-4 mb-2 w-[240px] bg-white rounded-[16px] shadow-[0_8px_30px_rgb(0,0,0,0.12)] border border-zinc-100 py-2 z-50 animate-in fade-in slide-in-from-bottom-2">
                        {/* Các tùy chọn menu chuẩn thiết kế */}
                        <button className="w-full flex items-center justify-between px-4 py-3 hover:bg-zinc-50 transition-colors">
                            <span className="font-medium text-[15px]">Settings</span>
                            <Settings size={18} />
                        </button>

                        <button className="w-full flex items-center justify-between px-4 py-3 hover:bg-zinc-50 transition-colors">
                            <span className="font-medium text-[15px]">Appearance</span>
                            <Moon size={18} />
                        </button>

                        <button className="w-full flex items-center justify-between px-4 py-3 hover:bg-zinc-50 transition-colors text-red-500">
                            <span className="font-medium text-[15px]">Report a problem</span>
                            <MessageSquareWarning size={18} />
                        </button>

                        <div className="h-[1px] bg-zinc-100 my-1 mx-2" />

                        <button onClick={handleLogout} className="w-full flex items-center justify-between px-4 py-3 hover:bg-zinc-50 transition-colors">
                            <span className="font-medium text-[15px]">Log out</span>
                            <LogOut size={18} />
                        </button>
                    </div>
                )}

                {/* Sử dụng lại ActionButton để kích hoạt menu */}
                <ActionButton
                    icon={<Menu />}
                    ariaLabel="Settings"
                    onClick={() => setIsMenuOpen(!isMenuOpen)}
                    className={isMenuOpen ? 'text-black' : 'text-zinc-500'}
                />
            </div>
        </aside>
    );
}
