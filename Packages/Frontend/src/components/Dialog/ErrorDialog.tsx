import React from 'react';

export interface BackendErrorResponse {
    isSuccess: boolean;
    statusCode: number | null;
    message: string;
    traceId: string | null;
    errors?: string[];
}

interface ErrorDialogProps {
    isOpen: boolean;
    onClose: () => void;
    errorData: BackendErrorResponse | null;
}

const ErrorDialog = ({ isOpen, onClose, errorData }: ErrorDialogProps) => {
    if (!isOpen || !errorData) return null;

    return (
        <div className="fixed inset-0 z-50 flex items-center justify-center p-4 bg-black/50 backdrop-blur-sm">
            <div className="bg-white rounded-[24px] w-full max-w-md overflow-hidden shadow-2xl animate-in fade-in zoom-in duration-200">
                {/* Header lỗi */}
                <div className="bg-red-50 p-6 flex items-center gap-4 border-b border-red-100">
                    <div className="w-12 h-12 bg-red-100 rounded-full flex items-center justify-center text-red-600">
                        <svg
                            xmlns="http://www.w3.org/2000/svg"
                            fill="none"
                            viewBox="0 0 24 24"
                            strokeWidth={2}
                            stroke="currentColor"
                            className="w-6 h-6"
                        >
                            <path
                                strokeLinecap="round"
                                strokeLinejoin="round"
                                d="M12 9v3.75m9-.75a9 9 0 1 1-18 0 9 9 0 0 1 18 0Zm-9 3.75h.008v.008H12v-.008Z"
                            />
                        </svg>
                    </div>
                    <div>
                        <h3 className="text-lg font-bold text-gray-900">System Message</h3>
                        <p className="text-sm text-red-600 font-medium">Error Code: {errorData.statusCode}</p>
                    </div>
                </div>

                {/* Nội dung lỗi */}
                <div className="p-6">
                    <p className="text-gray-700 font-medium mb-3">{errorData.message}</p>

                    {/* Hiển thị chi tiết danh sách lỗi (errors) nếu có */}
                    {errorData.errors && errorData.errors.length > 0 && (
                        <ul className="bg-gray-50 rounded-lg p-3 border border-gray-100 space-y-1">
                            {errorData.errors.map((err, index) => (
                                <li key={index} className="text-sm text-gray-600 flex gap-2">
                                    <span className="text-red-400">•</span> {err}
                                </li>
                            ))}
                        </ul>
                    )}

                    <p className="mt-4 text-[10px] text-gray-400 font-mono">Trace ID: {errorData.traceId}</p>
                </div>

                {/* Nút đóng */}
                <div className="p-4 bg-gray-50 flex justify-end">
                    <button
                        onClick={onClose}
                        className="px-6 py-2 bg-gray-900 text-white rounded-full font-semibold hover:bg-gray-800 transition-colors"
                    >
                        Understood
                    </button>
                </div>
            </div>
        </div>
    );
};

export default ErrorDialog;
