using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services.Interfaces
{
	public interface IFileService
	{
		Task<string> SaveTempAsync(IFormFile file);
		Task<string> MoveToPermanentAsync(string tempFileName);
		Task DeleteAsync(string path);

	}
}