import { useEffect, useRef } from "react";

interface Props {
    onIntersect: () => void;
    hasMore: boolean;
}

const InfiniteScrollDetector = ({ onIntersect, hasMore }: Props) => {
    // useRef để cắm mốc render tiếp, khi tới cái div đặt mốc
    //  (isIntersecting === true), trình duyệt sẽ gọi hàm onIntersect (chính là loadMore).
    const detectorRef = useRef<HTMLDivElement>(null);

    useEffect(() => {
        if (!hasMore) return;

        // IntersectionObserver check phần tử có nằm trong viewport ko
        const observer = new IntersectionObserver(
            (entries) => {
                if (entries[0].isIntersecting) {
                    onIntersect();
                }
            },
            { threshold: 1.0 } // Kích hoạt khi toàn bộ div hiện ra
        );

        if (detectorRef.current) {
            observer.observe(detectorRef.current);
        }

        return () => observer.disconnect();
    }, [onIntersect, hasMore]);

    return hasMore ? (
        <div ref={detectorRef} className="h-10 flex items-center justify-center">
            <span className="text-sm text-gray-400">Đang tải thêm...</span>
        </div>
    ) : (
        <div className="py-8 text-center text-gray-400 text-sm">
            Bạn đã xem hết bài đăng.
        </div>
    );
};

export default InfiniteScrollDetector;