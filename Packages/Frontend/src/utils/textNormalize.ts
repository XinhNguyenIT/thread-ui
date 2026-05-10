// Chuyển text loại bỏ dấu, vd thiếu => thieu
export const textNormalize = (str: string): string => {
	return str
		.normalize("NFD")
		.replace(/[\u0300-\u036f]/g, "")
		.replace(/đ/g, "d")
		.replace(/Đ/g, "D");
};