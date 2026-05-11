import { GenderTypeEnum } from "@/common/genderTypeEnum";
import { PrivacyTypeEnum } from "@/common/privacyTypeEnum";
import AvatarUpload from "@/components/Avatar/AvatarUpload";
import BaseButton from "@/components/Button/BaseButton";
import ContentUILayout from "@/components/ContentUILayout";
import CreatePostForm from "@/components/Forms/CreatePostForm";
import UpdateProfileForm from "@/components/Forms/UpdateProfileForm";
import { PostProps } from "@/components/PostList/Post";
import Posts from "@/components/PostList/Posts";
import { useAppSelector } from "@/hooks/useAppSelector";
import { textNormalize } from "@/utils/textNormalize";
import { useState } from "react";
import { BsInstagram } from "react-icons/bs";

const tabs = ["Thread", "Câu trả lời", "File phương tiện", "Bài đăng lại"];
type Tab = (typeof tabs)[number];

const MOCK_USER_DATA = {
    email: "thikinh.dev@gmail.com",
    firstName: "Kính",
    lastName: "Thị",
    gender: GenderTypeEnum.MALE, 
    birthday: "2002-01-01",
    avatarSrc: "",
};

const MOCK_USER_POST: PostProps[] = [{
    author : {
        userId: '11111111-1111-1111-1111-111111111111', 
        lastName: 'Demo', 
        firstName: '1', 
        gender: GenderTypeEnum.MALE
    },
    caption : "Nội dung này vi phạm tiêu chuẩn cộng đồng và đã bị báo cáo.",
    commentsCount : 0,
    createAt : "2026-05-08T13:43:21.2484509",
    isAvatar: false,
    likesCount: 0,
    medias: [],
    postId:  18,
    privacySetting: PrivacyTypeEnum.PRIVATE
}]

const ProfilePage = () => {
    const [activeTab, setActiveTab] = useState<Tab>("Thread");
    const [isFormOpen, setIsFormOpen] = useState(false);
    
    const userFromStore = useAppSelector((state) => state.auth.information)
    const user = userFromStore || MOCK_USER_DATA;

    console.log("user data: ", user)

    const handleCloseForm = () => {
        setIsFormOpen(false);
    };

    const handleRefresh = () => {
        // window.location.reload(); // Hoặc gọi hàm fetch lại data từ Redux - need to find out
    };

    return (
        <ContentUILayout>
            <div className="flex flex-col gap-8">
                
                {/* User Info */}
                <div className="flex flex-col gap-5">
                    <div className="flex items-center justify-between">
                        <div className="flex flex-col gap-1">
                            <h3 className="text-2xl font-bold">{`${user.lastName} ${user.firstName}`}</h3>
                            <h5 className="text-[15px]">{textNormalize(user.lastName).toLowerCase()}.{textNormalize(user.firstName).toLowerCase()}</h5>
                        </div>
                        
                        <AvatarUpload 
                            currentSrc={user.avatarSrc} 
                            onSuccess={handleRefresh} 
                        />
                    </div>
                    <div className="flex items-center justify-between">
                        <p className="text-gray-500">-- người theo dõi</p>
                        <div className="flex gap-2">
                            <BsInstagram size={24} />
                        </div>
                    </div>
                    <BaseButton variant="outline" onClick={() => setIsFormOpen(true)}>Chỉnh sửa trang cá nhân</BaseButton>
                    {isFormOpen && (
                        <UpdateProfileForm
                            initialData={user} 
                            onClose={handleCloseForm} 
                        />
                    )}
                </div>

                {/* Tabs */}
                <div>
                    <div className="border-b border-neutral-200">
                        <div className="flex">
                            {tabs.map((tab) => (
                                <button key={tab} onClick={() => setActiveTab(tab)}
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
                    <div className="min-h-75">
                        {activeTab === "Thread" && (
                            <div className="py-4">
                                <CreatePostForm />
                                
                                {/* Gọi danh sách post, mặc định load 5 bài */}
                                <Posts pageSize={5} />
                            </div>
                        )}

                        {activeTab === "Câu trả lời" && (
                            <div className="py-16 text-center">
                                <p>Chưa có câu trả lời nào</p>
                            </div>
                            
                        )}

                        {activeTab === "File phương tiện" && (
                            <div className="py-16 text-center">
                                <p>Chưa có file phương tiện nào</p>
                            </div>
                            
                        )}

                        {activeTab === "Bài đăng lại" && (
                            <div className="py-16 text-center">
                                <p>Chưa có bài đăng lại nào</p>
                            </div>
                            
                        )}
                    </div>
                </div>
                

            </div>
        </ContentUILayout>
    )
}

export default ProfilePage;
