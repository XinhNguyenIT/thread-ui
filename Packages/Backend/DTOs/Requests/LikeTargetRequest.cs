using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Enums;

namespace Backend.DTOs.Requests
{
	public class LikeTargetRequest
	{
		public int TargetId { get; set; }
		public TargetTypeEnum TargetType { get; set; }
	}
}