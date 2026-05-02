import React, { useState } from 'react';

type ImageRounded = 'none' | 'md' | 'lg' | 'xl' | 'full';
type ImageFit = 'cover' | 'contain';

type ImageProps = {
    src: string;
    alt: string;
    fallbackSrc?: string;
    className?: string;
    rounded?: ImageRounded;
    fit?: ImageFit;
    onClick?: () => void;
};

export default function Image({
    src,
    alt,
    fallbackSrc = '/images/ana1.jpg',
    className = '',
    rounded = 'lg',
    fit = 'cover',
    onClick,
}: ImageProps) {
    const [imgSrc, setImgSrc] = useState(src);

    const roundedStyles = {
        none: 'rounded-none',
        md: 'rounded-md',
        lg: 'rounded-lg',
        xl: 'rounded-xl',
        full: 'rounded-full',
    };

    const fitStyles = {
        cover: 'object-cover',
        contain: 'object-contain',
    };

    return (
        <img
            src={imgSrc}
            alt={alt}
            onClick={onClick}
            onError={() => setImgSrc(fallbackSrc)}
            className={`block w-full ${roundedStyles[rounded]} ${fitStyles[fit]} ${className}`}
        />
    );
}
