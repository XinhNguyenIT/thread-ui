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
        const wrapperBase = 'flex w-full items-center gap-3 border bg-white transition-colors';

        const wrapperVariant = {
            default: 'rounded-2xl border-zinc-300 px-4 py-3 focus-within:border-zinc-500',
            search: 'rounded-full border-zinc-200 px-5 py-4 focus-within:border-zinc-400',
        };

        const inputBase = 'w-full bg-transparent text-[16px] text-zinc-900 outline-none placeholder:text-zinc-400';

        return (
            <div className={`flex w-full flex-col gap-2 ${wrapperClassName}`}>
                {label && (
                    <label htmlFor={id} className="text-sm font-medium text-zinc-700">
                        {label}
                        {required && <span className="text-red-500"> *</span>}
                    </label>
                )}

                <div
                    className={`${wrapperBase} ${wrapperVariant[variant]} ${
                        error ? 'border-red-500' : ''
                    } ${className}`}
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

                {error && <span className="text-sm text-red-500">{error}</span>}
            </div>
        );
    },
);

Input.displayName = 'Input';

export default Input;
