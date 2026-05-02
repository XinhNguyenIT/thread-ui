import React from 'react';

type AvatarProps = {
    src?: string;
    alt?: string; // Thêm dấu ? để không bắt buộc phải truyền alt ở mọi nơi
    onClick?: () => void;
    className?: string;
    size?: 'sm' | 'md' | 'lg' | number;
};

export default function Avatar({
    src,
    alt = 'User Avatar', // Giá trị mặc định cho alt
    onClick,
    className = '',
    size = 'md',
}: AvatarProps) {
    // Đảm bảo đường dẫn fallback chính xác (ví dụ để trong thư mục public)
    const fallback = '/images/ana1.jpg';

    const sizeClasses = {
        sm: 'h-9 w-9', // Khoảng 36px
        md: 'h-10 w-10', // Khoảng 40px
        lg: 'h-12 w-12', // Khoảng 48px
    };

    const selectedSizeClass = typeof size === 'string' ? sizeClasses[size] : '';
    const customSizeStyle = typeof size === 'number' ? { width: size, height: size } : {};

    return (
        <img
            src={src || fallback}
            alt={alt}
            onClick={onClick}
            style={customSizeStyle}
            className={`rounded-full object-cover border border-zinc-200 transition-opacity ${
                onClick ? 'cursor-pointer hover:opacity-80' : ''
            } ${selectedSizeClass} ${className}`}
        />
    );
}
