import { Link, useNavigate } from 'react-router-dom'; // Thêm useNavigate vào đây
import { useForm } from 'react-hook-form';
import { ChevronDown, CircleHelp } from 'lucide-react';
import { register as registerUser } from '@/services/authService';
import Input from '@/components/Input';

// Định nghĩa kiểu dữ liệu (Để ở ngoài là đúng)
type RegisterFormValues = {
    contact: string;
    password: string;
    month: string;
    day: string;
    year: string;
    fullName: string;
    username: string;
};

const months = [
    'January',
    'February',
    'March',
    'April',
    'May',
    'June',
    'July',
    'August',
    'September',
    'October',
    'November',
    'December',
];
const days = Array.from({ length: 31 }, (_, i) => String(i + 1));
const years = Array.from({ length: 100 }, (_, i) => String(new Date().getFullYear() - i));

export default function RegisterPage() {
    // ĐÚNG: Khai báo navigate bên trong hàm Component
    const navigate = useNavigate();

    const {
        register,
        handleSubmit,
        formState: { errors, isSubmitting },
    } = useForm<RegisterFormValues>();

    // ĐÚNG: Đưa hàm onSubmit vào bên trong để sử dụng được navigate và registerUser
    const onSubmit = async (data: RegisterFormValues) => {
        try {
            const nameParts = data.fullName.split(' ');
            const firstName = nameParts[0];
            const lastName = nameParts.slice(1).join(' ') || ' ';

            const payload = {
                firstName: firstName,
                lastName: lastName,
                email: data.contact,
                password: data.password,
                role: 'USER',
            };

            const response = await registerUser(payload);
            console.log('Đăng ký thành công:', response);
            alert('Đăng ký thành công!');
            navigate('/login');
        } catch (error: any) {
            console.error('Lỗi đăng ký:', error.response?.data);
            alert('Lỗi: ' + (error.response?.data?.message || 'Không thể đăng ký'));
        }
    };

    return (
        <div className="min-h-screen bg-[#f5f5f5] flex justify-center py-12 px-4 font-sans">
            <div className="w-full max-w-[620px]">
                {/* Header */}
                <div className="text-center mb-4">
                    <h1 className="text-[28px] font-bold text-[#1c1e21] tracking-tight">Create your account</h1>
                    <p className="text-[16px] text-[#65676b] leading-tight px-10">
                        Sign up to see photos and videos from your friends.
                    </p>
                </div>

                <form onSubmit={handleSubmit(onSubmit)} className="flex flex-col space-y-6">
                    {/* Sử dụng Component Input mới của bạn */}
                    <Input
                        label="Mobile number or email"
                        placeholder="Mobile number or email"
                        id="contact"
                        inputClassName="h-[50px]"
                        error={errors.contact ? 'Mobile number or email is required' : ''}
                        {...register('contact', { required: true })}
                    />

                    <Input
                        label="Password"
                        type="password"
                        placeholder="Password"
                        id="password"
                        inputClassName="h-[50px]"
                        error={errors.password ? 'Password is required' : ''}
                        {...register('password', { required: true })}
                    />

                    {/* Birthday Section - Giữ nguyên logic cũ vì nó dùng Select */}
                    <div className="pt-2">
                        <div className="flex items-center gap-1 mb-3 ml-2">
                            <span className="text-[20px] font-bold text-[#1c1e21]">Birthday</span>
                            <CircleHelp className="h-6 w-6 text-[#1c1e21] opacity-60 cursor-pointer" />
                        </div>
                        <div className="grid grid-cols-3 gap-3">
                            {[
                                { name: 'month', data: months, label: 'Month' },
                                { name: 'day', data: days, label: 'Day' },
                                { name: 'year', data: years, label: 'Year' },
                            ].map((item) => (
                                <div key={item.name} className="relative">
                                    <select
                                        className="h-[50px] w-full appearance-none rounded-[12px] border border-[#d1d5db] bg-white px-4 text-[16px] text-[#1c1e21] outline-none focus:border-[#1877f2]"
                                        {...register(item.name as any, { required: true })}
                                    >
                                        <option value="">{item.label}</option>
                                        {item.data.map((val) => (
                                            <option key={val} value={val}>
                                                {val}
                                            </option>
                                        ))}
                                    </select>
                                    <ChevronDown className="pointer-events-none absolute right-4 top-1/2 h-6 w-6 -translate-y-1/2 text-gray-500" />
                                </div>
                            ))}
                        </div>
                    </div>

                    <Input
                        label="Name"
                        placeholder="Full name"
                        id="fullName"
                        inputClassName="h-[50px]"
                        error={errors.fullName ? 'Full name is required' : ''}
                        {...register('fullName', { required: true })}
                    />

                    <Input
                        label="Username"
                        placeholder="Username"
                        id="username"
                        inputClassName="h-[50px]"
                        error={errors.username ? 'Username is required' : ''}
                        {...register('username', { required: true })}
                    />

                    {/* Submit Button */}
                    <div className="pt-2">
                        <button
                            type="submit"
                            disabled={isSubmitting}
                            className="h-[50px] w-full rounded-full bg-[#262626] text-[20px] font-bold text-white transition hover:bg-[#1877f2] active:scale-[0.98] disabled:bg-gray-400"
                        >
                            {isSubmitting ? 'Registering...' : 'Register'}
                        </button>
                    </div>

                    {/* Footer Link */}
                    <div className="text-center pt-1">
                        <p className="text-[20px] text-[#1c1e21]">
                            Already have an account?{' '}
                            <Link to="/login" className="font-bold hover:underline">
                                Log in
                            </Link>
                        </p>
                    </div>
                </form>
            </div>
        </div>
    );
}
