import dayjs from "dayjs";
import relativeTime from "dayjs/plugin/relativeTime";
import "dayjs/locale/vi";

// Format thời gian cho bài viết => 5m, 10h, vv

export const formatPostTime = (date: string) => {
    const now = dayjs();
    const postDate = dayjs(date);

    const seconds = now.diff(postDate, "second");
    const minutes = now.diff(postDate, "minute");
    const hours = now.diff(postDate, "hour");
    const days = now.diff(postDate, "day");
    const weeks = now.diff(postDate, "week")
    const months = now.diff(postDate, "month")
    const years = now.diff(postDate, "year")

    if (seconds < 60) return "just now";
    if (minutes < 60) return `${minutes} minute`;
    if (hours < 24) return `${hours} hours`;
    if (days < 7) return `${days} days`;
    if (weeks < 4) return `${weeks} weeks`;
    if (months < 12) return `${months} months`;
    if (years < 1000) return `${years} years`;

    return postDate.format("DD/MM/YYYY");
};