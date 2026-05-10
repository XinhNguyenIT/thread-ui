import { updateUserProfile, UpdateUserRequest } from '@/api/user/userService';
import { useState } from 'react';

// update thông tin user
export const useUserProfile = () => {
    const [isSubmitting, setIsSubmitting] = useState(false);
    const [error, setError] = useState<string | null>(null);

    const updateInfo = async (data: UpdateUserRequest, onSuccess?: () => void) => {
        setIsSubmitting(true);
        setError(null);
        try {
            await updateUserProfile(data);
            
            if (onSuccess) {
                onSuccess();
            }
            
        } catch (err: any) {
            setError(err?.message || "Cập nhật thất bại");
            console.error("Hook Error:", err);
        } finally {
            setIsSubmitting(false);
        }
    };

    return { updateInfo, isSubmitting, error };
};