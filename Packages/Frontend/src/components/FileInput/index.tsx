/*Input chọn ảnh hoặc file.

Dùng cho: upload ảnh bài post, avatar, story, ảnh cover/profile */

import React, { useRef, useState } from 'react';

type FileInputProps = {
    label?: string;
    accept?: string;
    previewType?: 'image' | 'avatar';
    buttonText?: string;
    className?: string;
    disabled?: boolean;
    onFileSelect?: (file: File | null) => void;
};

export default function FileInput({
    label = 'Upload file',
    accept = 'image/*',
    previewType = 'image',
    buttonText = 'Choose file',
    className = '',
    disabled = false,
    onFileSelect,
}: FileInputProps) {
    const inputRef = useRef<HTMLInputElement | null>(null);
    const [previewUrl, setPreviewUrl] = useState<string>('');
    const [error, setError] = useState<string>('');

    const handleOpenFilePicker = () => {
        if (disabled) return;
        inputRef.current?.click();
    };

    const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const file = e.target.files?.[0];

        if (!file) {
            setPreviewUrl('');
            setError('');
            onFileSelect?.(null);
            return;
        }

        if (!file.type.startsWith('image/')) {
            setError('Only image files are allowed.');
            setPreviewUrl('');
            onFileSelect?.(null);
            return;
        }

        setError('');
        const objectUrl = URL.createObjectURL(file);
        setPreviewUrl(objectUrl);
        onFileSelect?.(file);
    };

    const handleRemoveFile = () => {
        setPreviewUrl('');
        setError('');
        if (inputRef.current) {
            inputRef.current.value = '';
        }
        onFileSelect?.(null);
    };

    return (
        <div className={`flex flex-col gap-3 ${className}`}>
            {label && <label className="text-sm font-medium text-zinc-700">{label}</label>}

            <input
                ref={inputRef}
                type="file"
                accept={accept}
                className="hidden"
                onChange={handleFileChange}
                disabled={disabled}
            />

            <div className="flex items-center gap-3">
                <button
                    type="button"
                    onClick={handleOpenFilePicker}
                    disabled={disabled}
                    className={`rounded-2xl border border-zinc-300 px-4 py-2 text-[16px] font-medium transition-colors ${
                        disabled ? 'cursor-not-allowed opacity-50' : 'hover:bg-zinc-100'
                    }`}
                >
                    {buttonText}
                </button>

                {previewUrl && (
                    <button
                        type="button"
                        onClick={handleRemoveFile}
                        className="text-sm font-medium text-red-500 hover:opacity-80"
                    >
                        Remove
                    </button>
                )}
            </div>

            {error && <p className="text-sm text-red-500">{error}</p>}

            {previewUrl && previewType === 'image' && (
                <img
                    src={previewUrl}
                    alt="Preview"
                    className="mt-2 h-60 w-full rounded-2xl border border-zinc-200 object-cover"
                />
            )}

            {previewUrl && previewType === 'avatar' && (
                <img
                    src={previewUrl}
                    alt="Avatar preview"
                    className="mt-2 h-20 w-20 rounded-full border border-zinc-200 object-cover"
                />
            )}
        </div>
    );
}
