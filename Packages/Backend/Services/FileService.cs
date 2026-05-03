using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs.Internals;
using Backend.Enums;
using Backend.Mappers;
using Backend.Models;
using Backend.Repositories.Interfaces;
using Backend.Services.Interfaces;

namespace Backend.Services
{
	public class FileService : IFileService
	{
		private readonly string _tempPath = Path.Combine("wwwroot/uploads/temps");
		private readonly string _postPath = Path.Combine("wwwroot/uploads/posts");
		private readonly IUrlService _urlMapper;
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly IUnitOfWork _unitOfWork;
		private readonly UserContext _userContext;

		public FileService(IUrlService urlMapper, IUnitOfWork unitOfWork, UserContext userContext, IWebHostEnvironment webHostEnvironment)
		{
			_urlMapper = urlMapper;
			_unitOfWork = unitOfWork;
			_userContext = userContext;
			_webHostEnvironment = webHostEnvironment;
		}

		public async Task<string> SaveTempAsync(IFormFile file)
		{
			var userId = _userContext.UserId;

			var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);

			Directory.CreateDirectory(_tempPath);

			var ext = Path.GetExtension(file.FileName);
			var fileName = $"{Guid.NewGuid()}{ext}";

			var fullPath = Path.Combine(_tempPath, fileName);

			using var stream = new FileStream(fullPath, FileMode.Create);
			await file.CopyToAsync(stream);

			var newMedia = new Media
			{
				Status = MediaStatusEnum.PROCESSING,
				Src = fileName,
			};

			return _urlMapper.GetFullUrl(newMedia, user.Gender);
		}

		public Task<string> MoveToPermanentAsync(string tempFileName)
		{
			Directory.CreateDirectory(_postPath);

			var tempFullPath = Path.Combine(_tempPath, tempFileName);
			var newFileName = $"{Guid.NewGuid()}{Path.GetExtension(tempFileName)}";
			var newFullPath = Path.Combine(_postPath, newFileName);

			File.Move(tempFullPath, newFullPath);

			return Task.FromResult(newFileName);
		}

		public Task DeleteAsync(string fullPath)
		{
			try
			{
				var uri = new Uri(fullPath);
				var path = uri.AbsolutePath;

				var trimmedPath = path.TrimStart('/');
				var physicalPath = Path.Combine(_webHostEnvironment.WebRootPath, trimmedPath);

				if (File.Exists(physicalPath))
				{
					File.Delete(physicalPath);
				}
			}
			catch (Exception)
			{

			}
			return Task.CompletedTask;
		}
	}
}