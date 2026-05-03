import React, { forwardRef } from 'react';

type InputVariant = 'default' | 'search';

interface InputProps extends React.InputHTMLAttributes<HTMLInputElement> {
    label?: string;
    error?: string;
    leftIcon?: React.ReactNode;
    rightIcon?: React.ReactNode;
    variant?: InputVariant;
    wrapperClassName?: string;
    inputClassName?: string;
}

const Input = forwardRef<HTMLInputElement, InputProps>(
    (
        {
            label,
            error,
            leftIcon,
            rightIcon,
            variant = 'default',
            className = '',
            wrapperClassName = '',
            inputClassName = '',
            id,
            required,
            ...props
        },
        ref,
    ) => {
        // Base classes cho wrapper
        const wrapperBase = 'flex w-full items-center gap-3 border bg-white transition-all duration-200';

        // Định dạng bo góc và padding theo variant
        const variantStyles = {
            default: 'rounded-2xl px-4 py-3',
            search: 'rounded-full px-5 py-4',
        };

        // Logic xử lý màu sắc Border để không bị ghi đè (Quan trọng)
        const getBorderClass = () => {
            if (error) {
                return 'border-red-500 focus-within:border-red-500 ring-1 ring-red-500/20';
            }
            if (variant === 'search') {
                return 'border-zinc-200 focus-within:border-zinc-400';
            }
            return 'border-zinc-300 focus-within:border-zinc-500';
        };

        const inputBase = 'w-full bg-transparent text-[16px] text-zinc-900 outline-none placeholder:text-zinc-400';

        return (
            <div className={`flex w-full flex-col gap-1.5 ${wrapperClassName}`}>
                {/* Label Section */}
                {label && (
                    <label htmlFor={id} className="ml-1 text-sm font-semibold text-zinc-700">
                        {label}
                        {required && <span className="text-red-500"> *</span>}
                    </label>
                )}

                {/* Input Wrapper */}
                <div
                    className={`
                        ${wrapperBase} 
                        ${variantStyles[variant]} 
                        ${getBorderClass()} 
                        ${className}
                    `}
                >
                    {leftIcon && (
                        <span className="flex items-center justify-center text-zinc-400 [&>svg]:h-5 [&>svg]:w-5">
                            {leftIcon}
                        </span>
                    )}

                    <input
                        id={id}
                        ref={ref}
                        className={`${inputBase} ${inputClassName}`}
                        aria-invalid={!!error}
                        {...props}
                    />

                    {rightIcon && (
                        <span className="flex items-center justify-center text-zinc-400 [&>svg]:h-5 [&>svg]:w-5">
                            {rightIcon}
                        </span>
                    )}
                </div>

                {/* Error Message */}
                {error && (
                    <span className="ml-1 text-[13px] font-medium text-red-500 animate-in fade-in slide-in-from-top-1 duration-200">
                        {error}
                    </span>
                )}
            </div>
        );
    },
);

Input.displayName = 'Input';

export default Input;
