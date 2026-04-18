using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Enums;

namespace Backend.Models
{
	public class PostReport
	{
		public int PostReportId { get; set; }
		public int? PostId { get; set; }

		public required string UserId { get; set; }

		public ReportReasonEnum Reason { get; set; }

		public string? AdditionalDetails { get; set; }

		public virtual Post? Post { get; set; }
		public virtual User? User { get; set; }

	}
}