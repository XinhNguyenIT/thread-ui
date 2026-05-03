import React, { useEffect, useState } from 'react';
import { Link, Navigate, useNavigate } from 'react-router-dom';
// Sửa lại import cho đúng với file authService.ts bạn đã tạo

import Input from '@/components/Input';
import Button from '@/components/Button/BaseButton';
import Spinner from '@/components/Spinner';
import { useAppDispatch } from '@/hooks/useAppDispatch';
import { authActions } from '@/redux/actions/authActions';
import { useAppSelector } from '@/hooks/useAppSelector';

function Login() {
    const navigate = useNavigate();
    const dispatch = useAppDispatch();

    const user = useAppSelector((state) => state.auth.information);

    useEffect(() => {
        if (user == null) {
            const fetchUser = async () => {
                try {
                    await dispatch(authActions.getCurrentUser()).unwrap();
                } catch (err) {
                    console.error('Phiên đăng nhập hết hạn');
                }
            };
            fetchUser();
        }
    }, []);

    // 1. Khai báo State
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [isLoading, setIsLoading] = useState(false);

    const canSubmit = email.trim() && password.trim();

    // 2. Logic xử lý khi nhấn nút Log in
    const handleLogin = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        setIsLoading(true);
        try {
            const payload = {
                email,
                password,
            };
            await dispatch(authActions.login(payload)).unwrap();
            navigate('/');
        } catch (error: any) {
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
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
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
