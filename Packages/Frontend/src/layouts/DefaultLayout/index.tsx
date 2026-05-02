import React, { useState } from 'react';
import { Outlet, useLocation } from 'react-router-dom';
import Header from './Header';
import Sidebar from './Sidebar';

const DefaultLayout = () => {
    const location = useLocation();
    const [activeTab, setActiveTab] = useState('For you');

    const isHomePage = location.pathname === '/home' || location.pathname === '/';
    const pageTitle = location.pathname.substring(1).split('/')[0];
    const formattedTitle = pageTitle.charAt(0).toUpperCase() + pageTitle.slice(1);

    return (
        <div className="flex min-h-screen bg-white">
            <aside className="fixed left-0 top-0 bottom-0 w-[70px] border-r border-zinc-100 bg-white z-40">
                <Sidebar />
            </aside>

            <div className="flex-1 ml-[70px] flex flex-col min-w-0">
                <Header
                    type={isHomePage ? 'home' : 'other'}
                    title={isHomePage ? '' : formattedTitle}
                    activeTab={activeTab}
                    onTabChange={setActiveTab}
                />

                <main className="flex-1 w-full overflow-hidden">
                    {/* Outlet sẽ là nơi render Home, Search... 
                        context truyền activeTab xuống cho tụi nó lấy dùng */}
                    <Outlet context={{ activeTab }} />
                </main>
            </div>
        </div>
    );
};

export default DefaultLayout;
