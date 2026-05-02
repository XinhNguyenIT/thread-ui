import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
// Sửa lại import cho đúng với file authService.ts bạn đã tạo
import { login } from '@/services/authService';
import Input from '@/components/Input';
import Button from '@/components/Button/BaseButton';
import Spinner from '@/components/Spinner';

function Login() {
    const navigate = useNavigate();

    // 1. Khai báo State
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [isLoading, setIsLoading] = useState(false);

    const canSubmit = username.trim() && password.trim();

    // 2. Logic xử lý khi nhấn nút Log in
    const handleLogin = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        setIsLoading(true);
        try {
            // Gọi đúng hàm login và truyền đúng cấu trúc Object mà Backend chờ đợi
            const response = await login({ email: username, password: password });

            // Kiểm tra kết quả trả về từ Backend (Thường là response.data)
            if (response.data && response.data.token) {
                // Requirement F2: Lưu token vào localStorage để duy trì phiên đăng nhập
                localStorage.setItem('token', response.data.token);

                // Chuyển hướng về trang chủ sau khi đăng nhập thành công
                navigate('/');
            } else {
                alert('Login failed: Invalid response from server');
            }
        } catch (error: any) {
            console.error('Lỗi đăng nhập:', error.response?.data);
            // Hiển thị lỗi cụ thể từ Backend trả về (ví dụ: Sai mật khẩu)
            alert(error.response?.data?.message || 'Login failed. Please check your credentials.');
        } finally {
            setIsLoading(false);
        }
    };

    return (
        <div className="min-h-screen bg-[#f5f5f5] flex justify-center pt-[50px] font-sans">
            <div className="w-full max-w-[400px] px-5">
                <h1 className="text-[24px] font-bold mb-10 color-[#1c1e21] text-center">Log into with your account</h1>

                <form onSubmit={handleLogin} className="flex flex-col gap-3">
                    {/* Sử dụng Component Input đã có */}
                    <Input
                        type="text"
                        placeholder="Mobile number, username or email"
                        value={username}
                        onChange={(e) => setUsername(e.target.value)}
                        className="h-[50px]"
                        wrapperClassName="rounded-[12px]"
                    />

                    <Input
                        type="password"
                        placeholder="Password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        className="h-[50px]"
                        wrapperClassName="rounded-[12px]"
                    />

                    {/* 2. Thay thẻ <button> bằng Component Button của bạn */}
                    <div className="flex justify-center mt-5">
                        <Button
                            type="submit"
                            disabled={!canSubmit || isLoading}
                            className="h-[40px] w-[120px] rounded-[28px] text-[20px] font-semibold"
                            style={{ backgroundColor: canSubmit ? '#1877f2' : '#a9c4ef' }}
                        >
                            {/* 3. Sử dụng Component Spinner khi đang load */}
                            {isLoading ? <Spinner size="sm" color="white" /> : 'Log in'}
                        </Button>
                    </div>
                </form>

                <div className="text-center mt-5">
                    <Link
                        to="/forgot-password"
                        className="text-[16px] font-medium text-[#1c1e21] no-underline hover:underline"
                    >
                        Forgot password?
                    </Link>
                </div>

                <div className="mt-5 flex justify-center">
                    <Link to="/register">
                        {/* Sử dụng Component Button cho nút tạo tài khoản */}
                        <Button
                            variant="outline" // Giả sử bạn có variant này trong component Button
                            className="h-[40px] w-[300px] rounded-[28px] border-2 border-[#1877f2] text-[#1877f2] text-[22px] font-medium bg-white"
                        >
                            Create new account
                        </Button>
                    </Link>
                </div>
            </div>
        </div>
    );
}

export default Login;
