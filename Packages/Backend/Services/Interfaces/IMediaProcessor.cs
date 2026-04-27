using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend.Services.Interfaces
{
	public interface IMediaProcessor
	{
		public Task ProcessAsync(Media media);
	}
}