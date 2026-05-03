import { Link, useNavigate } from 'react-router-dom'; // Thêm useNavigate vào đây
import { useForm } from 'react-hook-form';
import { ChevronDown, CircleHelp } from 'lucide-react';
import Input from '@/components/Input';
import { RoleTypeEnum } from '@/common/roleTypeEnum';
import { GenderTypeEnum } from '@/common/genderTypeEnum';
import { useAppDispatch } from '@/hooks/useAppDispatch';
import { authActions } from '@/redux/actions/authActions';

// Định nghĩa kiểu dữ liệu (Để ở ngoài là đúng)
type RegisterFormValues = {
    email: string;
    password: string;
    month: string;
    day: string;
    year: string;
    firstName: string;
    lastName: string;
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
    const dispatch = useAppDispatch();

    const {
        register,
        handleSubmit,
        getValues,
        trigger,
        formState: { errors, isSubmitting },
    } = useForm<RegisterFormValues>({
        mode: 'onTouched',
    });

    const nameValidationRule = {
        validate: (value: string) => {
            const { firstName, lastName } = getValues();
            if (!firstName && !lastName) {
                return 'At least one name (First or Last) is required';
            }
            return true;
        },
    };

    const emailValidation = {
        required: 'Email is required', // Không được để trống
        pattern: {
            value: /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i, // Biểu thức chính quy (Regex)
            message: 'Invalid email address', // Nếu không đúng định dạng thì báo lỗi này
        },
    };

    const passwordValidation = {
        required: 'Password is required',
        minLength: {
            value: 6,
            message: 'Password must be at least 6 characters',
        },
        validate: {
            hasUpperCase: (value: string) =>
                /[A-Z]/.test(value) || 'Password must contain at least one uppercase letter',
            hasSpecialChar: (value: string) =>
                /[!@#$%^&*(),.?":{}|<>]/.test(value) || 'Password must contain at least one special character',
        },
    };

    // ĐÚNG: Đưa hàm onSubmit vào bên trong để sử dụng được navigate và registerUser
    const onSubmit = async (data: RegisterFormValues) => {
        try {
            const payload = {
                firstName: data.firstName,
                lastName: data.lastName,
                email: data.email,
                password: data.password,
                gender: GenderTypeEnum.UNKNOWN,
                roles: [RoleTypeEnum.USER],
                birthday: `${data.year}/${months.indexOf(data.month) + 1}/${data.day}`,
            };

            await dispatch(authActions.register(payload)).unwrap();
            navigate('/login');
        } catch (error: any) {}
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
                        label="Email"
                        placeholder="Email, ex: user@example.com"
                        id="email"
                        inputClassName="border-red"
                        error={errors.email?.message}
                        {...register('email', emailValidation)}
                    />

                    <Input
                        label="Password"
                        type="password"
                        placeholder="Password"
                        id="password"
                        inputClassName={errors.firstName || errors.lastName ? 'border-red-500' : ''}
                        error={errors.password?.message}
                        {...register('password', passwordValidation)}
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
                    <div className="flex gap-5">
                        <Input
                            label="First Name"
                            placeholder="First name"
                            id="firstName"
                            error={errors.firstName?.message || errors.lastName?.message}
                            {...register('firstName', {
                                ...nameValidationRule,
                                onChange: () => trigger('lastName'),
                            })}
                        />
                        <Input
                            label="Last Name"
                            placeholder="Last name"
                            id="lastName"
                            inputClassName={errors.firstName || errors.lastName ? 'border-red-500' : ''}
                            {...register('lastName', {
                                ...nameValidationRule,
                                onChange: () => trigger('firstName'),
                            })}
                        />
                    </div>

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
