using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Enums;

namespace Backend.DTOs.Responses
{
	public class MediaResponse
	{
		public int Id { get; set; }
		public MediaTypeEnum Type { get; set; }
		public required string Src { get; set; }
	}
}