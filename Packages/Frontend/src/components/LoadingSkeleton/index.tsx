import React from 'react';

type LoadingSkeletonVariant = 'line' | 'circle' | 'rect';

type LoadingSkeletonProps = {
    variant?: LoadingSkeletonVariant;
    className?: string;
};

export default function LoadingSkeleton({ variant = 'line', className = '' }: LoadingSkeletonProps) {
    const baseStyles = 'animate-pulse bg-zinc-200';

    const variantStyles = {
        line: 'h-4 w-full rounded-md',
        circle: 'h-10 w-10 rounded-full',
        rect: 'h-32 w-full rounded-2xl',
    };

    return <div className={`${baseStyles} ${variantStyles[variant]} ${className}`} />;
}
