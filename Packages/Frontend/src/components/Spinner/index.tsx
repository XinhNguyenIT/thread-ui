/* Là biểu tượng loading quay quay.

Dùng khi:

đang gọi API
đang submit form
đang tải dữ liệu

Ví dụ:

bấm nút đăng nhập xong hiện vòng tròn quay
đang tải danh sách post */
import React from 'react';

interface SpinnerProps {
    size?: 'sm' | 'md' | 'lg';
    color?: string;
    className?: string;
}

const Spinner = ({ size = 'md', color = 'border-current', className = '' }: SpinnerProps) => {
    // Định nghĩa kích thước
    const sizeClasses = {
        sm: 'h-4 w-4 border-2',
        md: 'h-8 w-8 border-3',
        lg: 'h-12 w-12 border-4',
    };

    return (
        <div className={`flex justify-center items-center ${className}`}>
            <div
                className={`
                    ${sizeClasses[size]}
                    ${color}
                    animate-spin 
                    rounded-full 
                    border-t-transparent
                `}
                role="status"
            >
                <span className="sr-only">Loading...</span>
            </div>
        </div>
    );
};

export default Spinner;
