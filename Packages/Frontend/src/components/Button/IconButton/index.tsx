/**
 * IconButton Component
 * Dùng cho: like, comment, share, close modal, back, more menu.
 * Đặc điểm: Bo tròn hoàn toàn (rounded-full), có hiệu ứng scale khi nhấn.
 */

import React from 'react';
import { Link, useNavigate } from 'react-router-dom';

type IconButtonVariant = 'default' | 'active' | 'soft';
type IconButtonSize = 'md' | 'sm'; // Thêm sm cho các icon nhỏ hơn

type IconButtonProps = {
    icon: React.ReactNode;
    ariaLabel: string;
    variant?: IconButtonVariant;
    size?: IconButtonSize;
    disabled?: boolean;
    className?: string;
    onClick?: (e: React.MouseEvent) => void; // Thêm event để stop propagation
    to?: string;
    goBack?: boolean;
};

export default function IconButton({
    icon,
    ariaLabel,
    variant = 'default',
    size = 'md',
    disabled = false,
    className = '',
    onClick,
    to,
    goBack = false,
}: IconButtonProps) {
    const navigate = useNavigate();

    // Hiệu ứng transition mượt và scale 90% khi nhấn (đặc trưng của Threads)
    const baseStyles =
        'inline-flex items-center justify-center transition-all duration-200 active:scale-90 outline-none';

    const sizeStyles = {
        sm: 'h-8 w-8',
        md: 'h-10 w-10', // Kích thước chuẩn cho các nút tương tác
    };

    const variantStyles = {
        // Chuyển sang rounded-full để hover hiện hình tròn thay vì bo góc vuông
        default: 'rounded-full bg-transparent text-zinc-500 hover:bg-zinc-100 hover:text-black',
        active: 'rounded-full bg-transparent text-black',
        soft: 'rounded-full bg-zinc-100 text-zinc-600 hover:bg-zinc-200',
    };

    const disabledStyles = disabled ? 'cursor-not-allowed opacity-40' : 'cursor-pointer';

    const finalClassName = `${baseStyles} ${sizeStyles[size]} ${variantStyles[variant]} ${disabledStyles} ${className}`;

    const handleClick = (e: React.MouseEvent) => {
        if (disabled) return;

        // Ngăn chặn sự kiện click lan ra các thẻ cha (ví dụ nhấn Like không làm mở bài viết)
        e.stopPropagation();

        if (goBack) {
            navigate(-1);
            return;
        }

        if (onClick) {
            onClick(e);
        }
    };

    // Chỉnh lại icon size khoảng 22px để cân đối với khung 40px
    const iconElement = (
        <span className="flex items-center justify-center [&>svg]:h-[22px] [&>svg]:w-[22px] [&>svg]:stroke-[2]">
            {icon}
        </span>
    );

    if (to && !disabled) {
        return (
            <Link to={to} aria-label={ariaLabel} className={finalClassName} onClick={(e) => e.stopPropagation()}>
                {iconElement}
            </Link>
        );
    }

    return (
        <button
            type="button"
            aria-label={ariaLabel}
            disabled={disabled}
            onClick={handleClick}
            className={finalClassName}
        >
            {iconElement}
        </button>
    );
}
