using Backend.Infrastructure;

public static class CliHandler
{
	public static async Task<bool> HandleAsync(WebApplication app, string[] args)
	{
		var command = args.FirstOrDefault()?.ToLower();
		var env = args.Skip(1).FirstOrDefault()?.ToLower();

		if (command != "seed") return false;

		var start = DateTime.Now;
		var validEnvs = new[] { "default", "test" };

		Console.WriteLine("🚀 Starting seeding process...");
		Console.WriteLine($"📦 Environment: {env}");

		if (string.IsNullOrEmpty(env) || !validEnvs.Contains(env))
		{
			Console.WriteLine("❌ Invalid environment (default/test)");
			return true;
		}

		try
		{
			using var scope = app.Services.CreateScope();
			var seeder = scope.ServiceProvider.GetRequiredService<IDatabaseSeeder>();

			await seeder.SeedAsync(env);

			Console.WriteLine("✅ Seeding completed successfully!");
		}
		catch (Exception ex)
		{
			Console.WriteLine("❌ Seeding failed!");
			Console.WriteLine($"💥 Error: {ex.Message}");
		}

		Console.WriteLine($"⏱️ Done in {(DateTime.Now - start).TotalSeconds}s");

		return true;
	}
}