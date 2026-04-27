
using Backend.Background.Queue;
using Backend.Enums;
using Backend.Repositories.Interfaces;
using Backend.Services.Interfaces;

namespace Backend.Background.Workers;

public class MediaWorker : BackgroundService
{
	private readonly IMediaQueue _queue;
	private readonly IServiceScopeFactory _scopeFactory;

	public MediaWorker(IMediaQueue queue, IServiceScopeFactory scopeFactory)
	{
		_queue = queue;
		_scopeFactory = scopeFactory;
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		while (!stoppingToken.IsCancellationRequested)
		{
			var mediaId = await _queue.DequeueAsync(stoppingToken);
			using (var scope = _scopeFactory.CreateScope())
			{
				var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
				var processor = scope.ServiceProvider.GetRequiredService<IMediaProcessor>();

				var media = await unitOfWork.MediaRepository.GetByIdAsync(mediaId);

				if (media == null) continue;

				try
				{
					await processor.ProcessAsync(media);
					await unitOfWork.CommitAsync();
				}
				catch (Exception ex)
				{
					media.Status = MediaStatusEnum.FAILED;
					await unitOfWork.CommitAsync();
				}
			}
		}
	}
}