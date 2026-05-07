import BaseButton from "@/components/Button/BaseButton";
import ContentUILayout from "@/components/ContentUILayout";
import Image from "@/components/Image";
import { useState } from "react";
import { BsInstagram } from "react-icons/bs";

const tabs = ["Thread", "Câu trả lời", "File phương tiện", "Bài đăng lại"];
type Tab = (typeof tabs)[number];

const ProfilePage = () => {
    //tab đang active
    const [activeTab, setActiveTab] = useState<Tab>("Thread");

    return (
        <ContentUILayout>
            <div className="flex flex-col gap-8">
                
                {/* User Info */}
                <div className="flex flex-col gap-5">
                    <div className="flex items-center justify-between">
                        <div className="flex flex-col gap-1">
                            <h3 className="text-2xl font-bold">Thị Kính</h3>
                            <h5>kinh.thi.5</h5>
                        </div>
                        <div className="size-24 rounded-full overflow-hidden">
                            <Image src="" alt="avatar" />
                        </div>
                    </div>
                    <div className="flex items-center justify-between">
                        <p>0 người theo dõi</p>
                        <div className="flex gap-2">
                            <BsInstagram size={24} />
                            <BsInstagram size={24} />
                        </div>
                    </div>
                    <BaseButton variant="outline">Chỉnh sửa trang cá nhân</BaseButton>
                </div>

                {/* Tabs */}
                <div>
                    <div className="border-b border-neutral-200">
                        <div className="flex">
                            {tabs.map((tab) => (
                                <button
                                    key={tab}
                                    onClick={() =>
                                        setActiveTab(tab)
                                    }
                                    className={`flex-1 py-3 text-sm font-medium transition-all border-b-2
                                        ${activeTab === tab
                                                ? "border-black text-black"
                                                : "border-transparent text-neutral-400 hover:text-black"
                                        }`
                                    }
                                >
                                    {tab}
                                </button>
                            ))}
                        </div>
                        
                    </div>

                    {/* Content */}
                    <div className="py-16 text-center min-h-75">
                        {activeTab === "Thread" && (
                            <p className="m-auto">Chưa có thread nào</p>
                        )}

                        {activeTab === "Câu trả lời" && (
                            <p>Chưa có câu trả lời nào</p>
                        )}

                        {activeTab === "File phương tiện" && (
                            <p>Chưa có file phương tiện nào</p>
                        )}

                        {activeTab === "Bài đăng lại" && (
                            <p>Chưa có bài đăng lại nào</p>
                        )}
                    </div>
                </div>
                

            </div>
        </ContentUILayout>
    )
}

export default ProfilePage;
