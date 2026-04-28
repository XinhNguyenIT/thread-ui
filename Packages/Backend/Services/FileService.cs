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
		private readonly IUnitOfWork _unitOfWork;
		private readonly UserContext _userContext;
		private readonly ILogger<FileService> _logger;

		public FileService(IUrlService urlMapper, IUnitOfWork unitOfWork, UserContext userContext, ILogger<FileService> logger)
		{
			_urlMapper = urlMapper;
			_unitOfWork = unitOfWork;
			_userContext = userContext;
			_logger = logger;
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

		public Task DeleteAsync(string fileName)
		{
			if (string.IsNullOrEmpty(fileName)) return Task.CompletedTask;
			var fullPath = Path.Combine(_postPath, fileName);

			try
			{
				if (File.Exists(fullPath))
				{
					File.Delete(fullPath);
				}
			}
			catch (IOException ex)
			{
				_logger.LogError(ex, "Cannot delete file: {FileName}", fileName);
			}

			return Task.CompletedTask;
		}
	}
}