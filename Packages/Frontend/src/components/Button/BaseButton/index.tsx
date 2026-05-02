/* Cho các nút cơ bản có chung nền tảng: click được, disabled, size, variant */

import React from 'react';
import { Link, useNavigate } from 'react-router-dom';

type ButtonVariant = 'primary' | 'outline' | 'ghost';

type BaseButtonProps = {
    children: React.ReactNode;
    variant?: ButtonVariant;
    disabled?: boolean;
    className?: string;
    // Sửa lỗi chính tả từ "tyle" thành "style"
    style?: React.CSSProperties;

    // Nút thường
    onClick?: () => void;

    // Chuyển trang
    to?: string;

    // Quay lại trang trước
    goBack?: boolean;

    // Hủy trạng thái hiện tại
    onCancel?: () => void;

    // Loại button trong form
    type?: 'button' | 'submit' | 'reset';
};

export default function BaseButton({
    children,
    variant = 'primary',
    disabled = false,
    className = '',
    style, // Đã nhận style ở đây
    onClick,
    to,
    goBack = false,
    onCancel,
    type = 'button',
}: BaseButtonProps) {
    const navigate = useNavigate();

    const baseStyles =
        'inline-flex items-center justify-center rounded-2xl px-4 py-2 font-medium text-[16px] transition-colors duration-200';

    const variantStyles = {
        primary: 'bg-black text-white hover:bg-zinc-800',
        outline: 'border border-zinc-300 bg-white text-black hover:bg-zinc-100',
        ghost: 'bg-transparent text-black hover:bg-zinc-100',
    };

    const disabledStyles = disabled ? 'cursor-not-allowed opacity-50' : 'cursor-pointer';

    const finalClassName = `${baseStyles} ${variantStyles[variant]} ${disabledStyles} ${className}`;

    const handleClick = () => {
        if (disabled) return;

        if (onCancel) {
            onCancel();
        }

        if (goBack) {
            navigate(-1);
            return;
        }

        if (onClick) {
            onClick();
        }
    };

    // Nếu có "to" thì render dạng Link
    if (to && !disabled) {
        return (
            <Link to={to} className={finalClassName} style={style}>
                {children}
            </Link>
        );
    }

    return (
        <button
            type={type}
            disabled={disabled}
            onClick={handleClick}
            className={finalClassName}
            style={style} // BẮT BUỘC PHẢI CÓ DÒNG NÀY để nhận style từ trang Login truyền vào
        >
            {children}
        </button>
    );
}
