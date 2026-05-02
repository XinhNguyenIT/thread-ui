/*Ô nhập văn bản nhiều dòng.
Dùng cho: tạo post, comment dài, bio profile, report content
Web social gần như chắc chắn cần cái này.*/
import React, { forwardRef } from 'react';

interface TextareaProps extends React.TextareaHTMLAttributes<HTMLTextAreaElement> {
    label?: string;
    error?: string;
    wrapperClassName?: string;
    textareaClassName?: string;
}

const Textarea = forwardRef<HTMLTextAreaElement, TextareaProps>(
    ({ label, error, className = '', wrapperClassName = '', textareaClassName = '', id, required, ...props }, ref) => {
        const wrapperBase = 'flex w-full flex-col gap-2';

        const textareaBase =
            'w-full rounded-2xl border border-zinc-300 bg-white px-4 py-3 text-[16px] text-zinc-900 outline-none transition-colors placeholder:text-zinc-400 resize-none focus:border-zinc-500';

        return (
            <div className={`${wrapperBase} ${wrapperClassName}`}>
                {label && (
                    <label htmlFor={id} className="text-sm font-medium text-zinc-700">
                        {label}
                        {required && <span className="text-red-500"> *</span>}
                    </label>
                )}

                <textarea
                    id={id}
                    ref={ref}
                    aria-invalid={!!error}
                    className={`${textareaBase} ${error ? 'border-red-500' : ''} ${className} ${textareaClassName}`}
                    {...props}
                />

                {error && <span className="text-sm text-red-500">{error}</span>}
            </div>
        );
    },
);

Textarea.displayName = 'Textarea';

export default Textarea;
