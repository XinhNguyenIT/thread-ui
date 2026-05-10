import React, { useState } from 'react';
import { useForm, useFieldArray } from 'react-hook-form';
import { useOutletContext } from 'react-router-dom';
import Button from '@/components/Button/BaseButton';
import Spinner from '@/components/Spinner';
import { PrivacyTypeEnum } from '@/common/privacyTypeEnum';
import { deleteFiles, uploadFiles } from '@/api/file/fileService';
import { postNewFeed } from '@/api/post/fileService';

// Định nghĩa kiểu dữ liệu cho Form
export interface CreatePostInput {
    Caption: string;
    PrivacySetting: PrivacyTypeEnum; // 1: Public, 2: Friends, 3: Private
    IsAvatar: boolean;
    Files: string[]; // Danh sách URL trả về từ server
}

const CreatePostForm = () => {
    const [isUploading, setIsUploading] = useState(false);

    const {
        register,
        handleSubmit,
        setValue,
        watch,
        reset,
        control,
        formState: { errors },
    } = useForm<CreatePostInput>({
        defaultValues: {
            Caption: '',
            PrivacySetting: PrivacyTypeEnum.PUBLIC,
            IsAvatar: false,
            Files: [],
        },
    });

    // Theo dõi danh sách file để hiển thị preview
    const files = watch('Files');

    // Hàm xử lý khi chọn File
    const handleFileChange = async (e: React.ChangeEvent<HTMLInputElement>) => {
        const selectedFiles = e.target.files;
        if (!selectedFiles || selectedFiles.length === 0) return;

        setIsUploading(true);
        try {
            // Giả lập gọi API Upload cho từng file
            const uploadPromises = Array.from(selectedFiles).map(async (file) => {
                const response = await uploadFiles(file);
                return response.data;
            });

            const urls = await Promise.all(uploadPromises);

            // Cập nhật mảng Files trong useForm
            setValue('Files', [...files, ...urls]);
        } catch (error) {
            alert('Upload file thất bại!');
        } finally {
            setIsUploading(false);
        }
    };

    const onSubmit = async (data: CreatePostInput) => {
        const payload = {
            Caption: data.Caption,
            PrivacySetting: data.PrivacySetting,
            IsAvatar: data.IsAvatar,
            Files: data.Files.map((url) => {
                return url.split('/').pop()!;
            }),
        };

        var response = await postNewFeed(payload);

        if (response.success) {
            reset();
            setValue('Files', []);
        }
        console.log(response);
    };

    return (
        <form onSubmit={handleSubmit(onSubmit)} className="bg-white p-4 rounded-xl border border-zinc-200">
            {/* Caption */}
            <textarea
                {...register('Caption', { required: 'Vui lòng nhập nội dung' })}
                placeholder="What's on your mind?"
                className="w-full p-3 border-none outline-none resize-none text-[15px]"
                rows={3}
            />

            {/* Privacy & IsAvatar Row */}
            <div className='flex justify-between items-center'>

                {/* Option upload */}
                <div className='flex gap-2'>
                    <div className="flex gap-4 items-center">
                        <select {...register('PrivacySetting')} className="p-2 border rounded-lg bg-gray-50 outline-none text-[12px]">
                            <option value={PrivacyTypeEnum.PUBLIC}>Public</option>
                            <option value={PrivacyTypeEnum.FRIEND}>Friends</option>
                            <option value={PrivacyTypeEnum.PRIVATE}>Private</option>
                        </select>
                    </div>

                    {/* File Upload Section */}
                    <div className="">
                        <label className="inline-block p-2 bg-blue-50 text-blue-600 rounded-lg cursor-pointer hover:bg-blue-100 text-[12px]">
                            <input
                                type="file"
                                multiple
                                className="hidden"
                                onChange={handleFileChange}
                                accept="image/*,video/*"
                            />
                            {isUploading ? <Spinner size="sm" /> : '📁 Add Photos/Videos'}
                        </label>

                    </div>
                </div>
                
                <Button type="submit" className=" py-2 bg-blue-500 text-white rounded-full font-bold">
                    Post
                </Button>
            </div>
            

            {/* Preview Files - Đi chung với file upload section*/}
            <div className="flex flex-wrap gap-2">
                {files.map((url, index) => (
                    <div key={index} className="relative w-20 h-20 mt-2">
                        <img src={url} className="w-full h-full object-cover rounded-lg" />
                        <button
                            type="button"
                            onClick={async () => {
                                var response = await deleteFiles(url);
                                if (response.success) {
                                    setValue(
                                        'Files',
                                        files.filter((_, i) => i !== index),
                                    );
                                }
                            }}
                            className="absolute -top-2 -right-2 bg-red-500 text-white rounded-full w-5 h-5 text-xs"
                        >
                            ×
                        </button>
                    </div>
                ))}
            </div>
            
        </form>
    );
};

export default CreatePostForm;
