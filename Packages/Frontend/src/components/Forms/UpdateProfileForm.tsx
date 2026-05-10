import React, { useState, useRef } from 'react';
import { Camera, ChevronRight, X } from 'lucide-react';
import Image from '../Image';
import { GenderTypeEnum } from '@/common/genderTypeEnum';

interface UpdateProfileProps {
    initialData: {
        firstName: string;
        lastName: string;
        gender: GenderTypeEnum;
        avatarSrc: string;
    };
    onClose: () => void;
}

const UpdateProfileForm: React.FC<UpdateProfileProps> = ({ initialData, onClose }) => {
    const [formData, setFormData] = useState(initialData);
    const [previewAvatar, setPreviewAvatar] = useState(initialData.avatarSrc);
    const fileInputRef = useRef<HTMLInputElement>(null);

    const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const file = e.target.files?.[0];
        if (file) {
            const url = URL.createObjectURL(file);
            setPreviewAvatar(url);
            // Để UpdateAvatarRequest here
        }
    };

    const handleSave = () => {
        // Để API update here
        console.log("Saving data:", formData);
        onClose();
    };

    return (
        <div className="fixed inset-0 z-50 flex items-center justify-center bg-black/80 backdrop-blur-sm p-4 transition">
            <div className="w-full max-w-[520px] bg-[#181818] border border-[#2d2d2d] rounded-[28px] text-[#f3f5f7] shadow-2xl">
                
                {/* Header */}
                <div className="relative flex items-center justify-center p-5 border-b border-[#2d2d2d]">
                    <button onClick={onClose} className="absolute left-6 text-sm hover:opacity-70 transition-opacity">
                        Hủy
                    </button>
                    <h2 className="font-bold text-base">Chỉnh sửa hồ sơ</h2>
                </div>

                {/* Body */}
                <div className="p-6">
                    <div className="bg-[#1e1e1e] border border-[#2d2d2d] rounded-[18px] overflow-hidden">
                        {/* Row: Name */}
                        <div className="flex p-4 border-b border-[#2d2d2d] items-start hover:bg-[#252525] transition-colors">
                            <div className="flex-1">
                                <label className="block text-[13px] font-bold mb-1">Tên</label>
                                <div className="flex gap-2">
                                    <input 
                                        type="text"
                                        placeholder="Họ"
                                        value={formData.lastName}
                                        onChange={(e) => setFormData({...formData, lastName: e.target.value})}
                                        className="bg-transparent outline-none w-full text-[14px] text-gray-300 focus:text-white"
                                    />
                                    <input 
                                        type="text"
                                        placeholder="Tên"
                                        value={formData.firstName}
                                        onChange={(e) => setFormData({...formData, firstName: e.target.value})}
                                        className="bg-transparent outline-none w-full text-[14px] text-gray-300 focus:text-white border-l border-[#333] pl-2"
                                    />
                                </div>
                            </div>
                            <div className="ml-4 cursor-pointer relative" onClick={() => fileInputRef.current?.click()}>
                                <div className='w-12 h-12 rounded-full object-cover overflow-hidden'>
                                    <Image src={previewAvatar} alt="avatar" />
                                </div>
                                
                                <div className="absolute -bottom-1 -right-1 bg-black border border-[#2d2d2d] p-1 rounded-full">
                                    <Camera size={12} />
                                </div>
                                <input 
                                    type="file" 
                                    ref={fileInputRef} 
                                    className="hidden" 
                                    onChange={handleFileChange} 
                                    accept="image/*" 
                                />
                            </div>
                        </div>

                        {/* Row: Gender */}
                        <div className="relative group">
                            <div className="p-4 flex items-center justify-between hover:bg-[#252525] transition-colors cursor-pointer border-t border-[#2d2d2d]">
                                <div className="flex-1">
                                    <label className="block text-[13px] font-bold mb-1">Giới tính</label>
                                    <div className="text-[14px] text-gray-400">
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
                    </div>

                    <button onClick={handleSave} className="w-full mt-6 bg-white text-black font-bold py-3 rounded-[15px] hover:bg-gray-200 transition-colors">
                        Xong
                    </button>
                </div>
                
            </div>
        </div>
    );
};

export default UpdateProfileForm;