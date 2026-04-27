namespace Backend.Background.Queue;

public interface IMediaQueue
{
	void Enqueue(int mediaId);
	ValueTask<int> DequeueAsync(CancellationToken cancellationToken);
}