using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using GestionApi.Data;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

Env.Load();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllers();

string adminHash = Env.GetString("ADMIN_PASSWORD");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseNpgsql(connectionString));
builder.Services.AddHealthChecks()
	.AddNpgSql(connectionString);
var app = builder.Build();

app.MapHealthChecks("/health");

using (var scope = app.Services.CreateScope()){
	var services = scope.ServiceProvider;
	var context = services.GetRequiredService<AppDbContext>();
	int retries = 3;
	while (retries > 0){
		try {
			context.Database.Migrate();
			Console.WriteLine("Conectado correctamente");
			retries = 0;
		}
		catch {
			Console.WriteLine("Error conectandose");
			Thread.Sleep(3000);
			retries--;
		}
	}
	
}


app.UseSwagger();
app.UseSwaggerUI();

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
