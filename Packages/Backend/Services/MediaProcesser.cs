using System.Diagnostics;
using Backend.Enums;
using Backend.Models;
using Backend.Services.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;

namespace Backend.Services;

public class MediaProcessor : IMediaProcessor
{
	private readonly string _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "posts");
	public async Task ProcessAsync(Media media)
	{
		if (!Directory.Exists(_uploadPath)) Directory.CreateDirectory(_uploadPath);

		var input = Path.Combine(_uploadPath, media.Src);
		var name = Path.GetFileNameWithoutExtension(media.Src);

		if (!File.Exists(input)) return;

		try
		{
			if (media.Type == MediaTypeEnum.IMAGE)
			{
				var outputName = $"{name}.webp";
				var outputPath = Path.Combine(_uploadPath, outputName);

				using var image = await Image.LoadAsync(input);
				await image.SaveAsync(outputPath, new WebpEncoder { Quality = 75 });

				media.ProcessedSrc = outputName;
			}
			else
			{
				var outputName = $"{name}.mp4";
				var outputPath = Path.Combine(_uploadPath, outputName);

				var success = await RunFfmpeg(input, outputPath);
				if (success) media.ProcessedSrc = outputName;
			}

			media.Status = MediaStatusEnum.DONE;

			File.Delete(input);
		}
		catch (Exception)
		{
			media.Status = MediaStatusEnum.FAILED;
		}
	}

	private async Task<bool> RunFfmpeg(string input, string output)
	{
		var startInfo = new ProcessStartInfo
		{
			FileName = "ffmpeg",
			Arguments = $"-i \"{input}\" -vcodec libx264 -crf 28 -preset fast -y \"{output}\"",
			RedirectStandardError = true,
			UseShellExecute = false,
			CreateNoWindow = true,
		};

		using var process = Process.Start(startInfo);
		if (process == null) return false;

		await process.WaitForExitAsync();
		return process.ExitCode == 0;
	}
}