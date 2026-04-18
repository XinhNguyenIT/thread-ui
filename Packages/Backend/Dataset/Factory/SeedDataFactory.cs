using Backend.Dataset.Interfaces;

public class SeedDataFactory
{
	private readonly IEnumerable<ISeedData> _seedData;

	public SeedDataFactory(IEnumerable<ISeedData> seedData)
	{
		_seedData = seedData;
	}

	public ISeedData Get(string? type)
	{
		var data = _seedData.FirstOrDefault(x => x.Type == type?.ToLower());

		if (data == null)
			throw new ArgumentException("Invalid seed type");

		return data;
	}
}