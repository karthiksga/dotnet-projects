using System.Threading.Channels;

namespace ChannelsConsoleApp;

public static class ChannelExtensions
{
	/// <summary>
	/// Tries to write an item to a channel with a timeout
	/// </summary>
	public static async Task<bool> TryWriteAsync<T>(
		this ChannelWriter<T> writer,
		T item,
		TimeSpan timeout,
		CancellationToken cancellationToken = default)
	{
		using var timeoutTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
		timeoutTokenSource.CancelAfter(timeout);

		try
		{
			await writer.WriteAsync(item, timeoutTokenSource.Token);
			return true;
		}
		catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
		{
			// Timeout occurred
			return false;
		}
	}

	/// <summary>
	/// Tries to read an item from a channel with a timeout
	/// </summary>
	public static async Task<(bool Success, T? Item)> TryReadAsync<T>(
		this ChannelReader<T> reader,
		TimeSpan timeout,
		CancellationToken cancellationToken = default)
	{
		using var timeoutTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
		timeoutTokenSource.CancelAfter(timeout);

		try
		{
			var item = await reader.ReadAsync(timeoutTokenSource.Token);
			return (true, item);
		}
		catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
		{
			// Timeout occurred
			return (false, default);
		}
		catch (ChannelClosedException)
		{
			// Channel was closed
			return (false, default);
		}
	}

	/// <summary>
	/// Batch reads items from a channel until a timeout occurs or the maximum batch size is reached
	/// </summary>
	public static async Task<List<T>> ReadBatchAsync<T>(
		this ChannelReader<T> reader,
		int maxBatchSize,
		TimeSpan timeout,
		CancellationToken cancellationToken = default)
	{
		var result = new List<T>(maxBatchSize);
		var sw = System.Diagnostics.Stopwatch.StartNew();

		while (result.Count < maxBatchSize && sw.Elapsed < timeout && !cancellationToken.IsCancellationRequested)
		{
			var remainingTime = timeout - sw.Elapsed;
			if (remainingTime <= TimeSpan.Zero)
			{
				break;
			}

			var (success, item) = await reader.TryReadAsync(remainingTime, cancellationToken);
			if (!success)
			{
				break; // Timeout or channel closed
			}

			if (item != null)
			{
				result.Add(item);
			}
		}

		return result;
	}
}
