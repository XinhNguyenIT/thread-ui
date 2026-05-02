/*Hộp thoại nổi.

Dùng cho:

xem ảnh lớn
xác nhận xóa post
edit profile
create post popup
report content*/
import React from 'react';

type ModalProps = {
    isOpen: boolean;
    onClose: () => void;
    children: React.ReactNode;
    title?: string;
    className?: string;
};

export default function Modal({ isOpen, onClose, children, title, className = '' }: ModalProps) {
    if (!isOpen) return null;

    return (
        <div className="fixed inset-0 z-50 flex items-center justify-center">
            {/* Overlay */}
            <div className="absolute inset-0 bg-black/40" onClick={onClose} />

            {/* Modal content */}
            <div className={`relative z-10 w-full max-w-xl rounded-3xl bg-white p-6 shadow-xl ${className}`}>
                <div className="mb-4 flex items-center justify-between">
                    {title ? <h2 className="text-lg font-semibold text-zinc-900">{title}</h2> : <div />}

                    <button
                        type="button"
                        onClick={onClose}
                        className="rounded-full p-2 text-zinc-500 transition hover:bg-zinc-100 hover:text-zinc-900"
                        aria-label="Close modal"
                    >
                        ✕
                    </button>
                </div>

                <div>{children}</div>
            </div>
        </div>
    );
}
