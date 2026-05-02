/*Cho kiểu icon + count như hàng nút dưới post

Ví dụ:like + 165, comment + 50, repost + 1, share + 7*/
import React from 'react';
import { Link, useNavigate } from 'react-router-dom';

type ActionButtonVariant = 'default' | 'active';

type ActionButtonProps = {
    icon?: React.ReactNode;
    ariaLabel?: string;
    count?: number | string;

    variant?: ActionButtonVariant;
    disabled?: boolean;
    className?: string;

    onClick?: () => void;
    to?: string;
    goBack?: boolean;
    children?: React.ReactNode;
    label?: string;
};

export default function ActionButton({
    icon,
    count,
    ariaLabel,
    variant = 'default',
    disabled = false,
    className = '',
    onClick,
    to,
    goBack = false,
}: ActionButtonProps) {
    const navigate = useNavigate();

    const baseStyles =
        'inline-flex items-center gap-2 bg-transparent text-[16px] font-normal transition-all duration-200';

    const variantStyles = {
        default: 'text-zinc-700 hover:opacity-70',
        active: 'text-black',
    };

    const disabledStyles = disabled ? 'cursor-not-allowed opacity-50' : 'cursor-pointer';

    const finalClassName = `${baseStyles} ${variantStyles[variant]} ${disabledStyles} ${className}`;

    const handleClick = () => {
        if (disabled) return;

        if (goBack) {
            navigate(-1);
            return;
        }

        if (onClick) {
            onClick();
        }
    };

    const content = (
        <>
            <span className="flex items-center justify-center [&>svg]:h-6 [&>svg]:w-6">{icon}</span>

            {count !== undefined && <span className="leading-none">{count}</span>}
        </>
    );

    if (to && !disabled) {
        return (
            <Link to={to} aria-label={ariaLabel} className={finalClassName}>
                {content}
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
            {content}
        </button>
    );
}
