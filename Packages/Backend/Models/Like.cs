using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Enums;

namespace Backend.Models
{
	public class Like
	{
		public int LikeId { get; set; }
		public required string UserId { get; set; }
		public int TargetId { get; set; }
		public virtual User? User { get; set; }
		public required TargetTypeEnum TargetType { get; set; }
	}
}