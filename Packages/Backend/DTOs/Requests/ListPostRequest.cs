using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs.Requests
{
	public class ListPostRequest
	{
		public int Page { get; set; } = 1;
		public int PageSize { get; set; } = 10;
	}
}