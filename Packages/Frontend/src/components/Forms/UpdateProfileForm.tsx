import React, { useState } from 'react';
import { ChevronRight } from 'lucide-react';
import { GenderTypeEnum } from '@/common/genderTypeEnum';
import { useUserProfile } from '@/hooks/useUserProfile';

interface UpdateProfileProps {
    initialData: {
        firstName: string;
        lastName: string;
        gender: GenderTypeEnum;
    };
    onClose: () => void;
    onSuccess?: () => void; // Callback để load lại data ở trang Profile sau khi save thành công
}

const UpdateProfileForm: React.FC<UpdateProfileProps> = ({ initialData, onClose, onSuccess }) => {
    const [formData, setFormData] = useState(initialData);
    const { updateInfo, isSubmitting } = useUserProfile();

    const handleSave = async () => {
        await updateInfo(formData, () => {
            onClose();
            // tùy mà sau sửa cái này
            window.location.reload(); 
        });
    };

    return (
        <div className="fixed inset-0 z-50 flex items-center justify-center bg-black/80 backdrop-blur-sm p-4 transition">
            <div className="w-full max-w-130 bg-[#181818] border border-[#2d2d2d] rounded-[28px] text-[#f3f5f7] shadow-2xl overflow-hidden">
                
                {/* Header */}
                <div className="relative flex items-center justify-center p-5 border-b border-[#2d2d2d]">
                    <button 
                        onClick={onClose} 
                        className="absolute left-6 text-sm hover:opacity-70 transition-opacity"
                        disabled={isSubmitting}
                    >
                        Hủy
                    </button>
                    <h2 className="font-bold text-base">Chỉnh sửa hồ sơ</h2>
                </div>

                {/* Body */}
                <div className="p-6">
                    <div className="bg-[#1e1e1e] border border-[#2d2d2d] rounded-[18px] overflow-hidden">
                        
                        {/* Row: Name (Lastname & Firstname) */}
                        <div className="flex p-4 border-b border-[#2d2d2d] items-start hover:bg-[#252525] transition-colors">
                            <div className="flex-1">
                                <label className="block text-[13px] font-bold mb-1 text-gray-400">Tên</label>
                                <div className="flex gap-2">
                                    <input 
                                        type="text"
                                        placeholder="Họ"
                                        value={formData.lastName}
                                        onChange={(e) => setFormData({...formData, lastName: e.target.value})}
                                        className="bg-transparent outline-none w-full text-[14px] text-white placeholder:text-zinc-600"
                                    />
                                    <input 
                                        type="text"
                                        placeholder="Tên"
                                        value={formData.firstName}
                                        onChange={(e) => setFormData({...formData, firstName: e.target.value})}
                                        className="bg-transparent outline-none w-full text-[14px] text-white border-l border-[#333] pl-2 placeholder:text-zinc-600"
                                    />
                                </div>
                            </div>
                        </div>

                        {/* Row: Gender */}
                        <div className="relative group p-4 flex items-center justify-between hover:bg-[#252525] transition-colors cursor-pointer border-t border-[#2d2d2d]">
                            <div className="flex-1">
                                <label className="block text-[13px] font-bold mb-1 text-gray-400">Giới tính</label>
                                <div className="text-[14px] text-white">
                                    {formData.gender === GenderTypeEnum.MALE && "Nam"}
                                    {formData.gender === GenderTypeEnum.FEMALE && "Nữ"}
                                    {formData.gender === GenderTypeEnum.OTHER && "Khác"}
                                    {formData.gender === GenderTypeEnum.UNKNOWN && "Không xác định"}
                                </div>
                            </div>
                            <select 
                                value={formData.gender}
                                onChange={(e) => setFormData({...formData, gender: e.target.value as GenderTypeEnum})}
                                className="absolute inset-0 w-full h-full opacity-0 cursor-pointer z-10 text-black"
                            >
                                <option value={GenderTypeEnum.MALE}>Nam</option>
                                <option value={GenderTypeEnum.FEMALE}>Nữ</option>
                                <option value={GenderTypeEnum.OTHER}>Khác</option>
                                <option value={GenderTypeEnum.UNKNOWN}>Không xác định</option>
                            </select>
                            <ChevronRight size={18} className="text-gray-600 group-hover:text-gray-300" />
                        </div>
                    </div>

                    <button 
                        onClick={handleSave} 
                        disabled={isSubmitting}
                        className={`w-full mt-6 bg-white text-black font-bold py-3 rounded-[15px] transition-colors ${isSubmitting ? 'opacity-50 cursor-not-allowed' : 'hover:bg-gray-200'}`}
                    >
                        {isSubmitting ? "Đang lưu..." : "Xong"}
                    </button>
                </div>
                
            </div>
        </div>
    );
};

export default UpdateProfileForm;