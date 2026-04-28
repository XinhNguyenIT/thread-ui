using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Enums;

namespace Backend.Helpers
{
	public static class TypeMediaHelper
	{
		public static MediaTypeEnum Get(string finalName)
		{
			var extension = Path.GetExtension(finalName).ToLowerInvariant();

			return extension switch
			{
				// Nhóm Image
				".jpg" or ".jpeg" or ".png" or ".gif" or ".bmp" or ".webp" => MediaTypeEnum.IMAGE,

				// Nhóm Video
				".mp4" or ".mov" or ".avi" or ".wmv" or ".flv" or ".mkv" => MediaTypeEnum.VIDEO,

				// Các loại khác hoặc mặc định
				".pdf" => MediaTypeEnum.PDF,
				_ => MediaTypeEnum.OTHER
			};
		}
	}
}