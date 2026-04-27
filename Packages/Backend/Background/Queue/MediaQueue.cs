using System.Threading.Channels;
using Backend.Background.Queue;

namespace Backend.Background.Queue;

public class MediaQueue : IMediaQueue
{
	private readonly Channel<int> _queue = Channel.CreateUnbounded<int>();

	public void Enqueue(int mediaId)
	{
		_queue.Writer.TryWrite(mediaId);
	}

	public async ValueTask<int> DequeueAsync(CancellationToken cancellationToken)
	{
		return await _queue.Reader.ReadAsync(cancellationToken);
	}
}