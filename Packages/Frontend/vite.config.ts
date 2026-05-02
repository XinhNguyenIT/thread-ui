import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';
import path from 'path';

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [react()],
    resolve: {
        alias: {
            '@': path.resolve(__dirname, './src'),
        },
    },
    css: {
        preprocessorOptions: {
            scss: {
                // Tùy chọn này giúp ẩn các cảnh báo từ các thư viện trong node_modules
                quietDeps: true,
                // Dập tắt cụ thể cảnh báo về "legacy-js-api" cho các bản Sass mới
                silenceDeprecations: ['legacy-js-api'],
            },
        },
    },
    // Chặn các log cảnh báo lặp lại quá nhiều trong Terminal
    logLevel: 'info',
});
