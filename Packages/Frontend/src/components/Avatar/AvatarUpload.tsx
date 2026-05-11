import React, { useRef, useState } from 'react';
import { Pencil, Loader2 } from 'lucide-react';
import Image from '../Image';
import { updateAvatar } from '@/api/user/userService';
import { uploadFiles } from '@/api/file/fileService'; // Giả sử đây là đường dẫn fileService của bạn

interface AvatarUploadProps {
    currentSrc: string;
    onSuccess?: () => void;
}

const AvatarUpload: React.FC<AvatarUploadProps> = ({ currentSrc, onSuccess }) => {
    const fileInputRef = useRef<HTMLInputElement>(null);
    const [isUploading, setIsUploading] = useState(false);

    const handleFileChange = async (e: React.ChangeEvent<HTMLInputElement>) => {
        const file = e.target.files?.[0];
        if (!file) return;

        const isConfirm = window.confirm("Bạn chắc chắn muốn đổi ảnh đại diện chứ?");
        if (!isConfirm) {
            e.target.value = ""; 
            return;
        }

        try {
            setIsUploading(true);

            const uploadRes = await uploadFiles(file);
            console.log("up pic:", uploadRes)

            if (uploadRes.success && uploadRes.data) {
                const imageUrl = uploadRes.data;
                console.log(imageUrl)

                // BƯỚC 2: Lấy URL từ bước 1 để cập nhật Profile User
                const updateAvatarRes = await updateAvatar({
                    file: imageUrl, 
                    privacy: "PUBLIC"
                });

                console.log("ket qua luu", updateAvatarRes)

                alert("Cập nhật ảnh đại diện thành công!");
                if (onSuccess) onSuccess();
            } else {
                throw new Error("Upload file không thành công");
            }
        } catch (error) {
            console.error("Lỗi cập nhật avatar:", error);
            alert("Không thể cập nhật ảnh đại diện. Vui lòng thử lại.");
        } finally {
            setIsUploading(false);
            if (fileInputRef.current) fileInputRef.current.value = ""; // Reset input
        }
    };

    return (
        <div 
            className={`group relative size-24 rounded-full overflow-hidden cursor-pointer border border-[#2d2d2d] bg-[#262626] 
                ${isUploading ? 'pointer-events-none opacity-80' : ''}`}
            onClick={() => !isUploading && fileInputRef.current?.click()}
        >
            <Image src={currentSrc} alt="avatar" className="w-full h-full object-cover transition group-hover:opacity-50" />

            {/* Lớp phủ khi hover hoặc đang loading */}
            <div className={`absolute inset-0 flex items-center justify-center transition-opacity bg-black/40 
                ${isUploading ? 'opacity-100' : 'opacity-0 group-hover:opacity-100'}`}>
                <div className="bg-white/20 p-2 rounded-full backdrop-blur-md">
                    {isUploading ? (
                        <Loader2 size={20} className="text-white animate-spin" />
                    ) : (
                        <Pencil size={20} className="text-white" />
                    )}
                </div>
            </div>

            <input 
                type="file" 
                ref={fileInputRef} 
                className="hidden" 
                accept="image/*" 
                onChange={handleFileChange} 
            />
        </div>
    );
};

export default AvatarUpload;