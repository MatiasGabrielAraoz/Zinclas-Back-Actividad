using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using GestionApi.Data;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

Env.Load();

var connectionString = $"Host={Env.GetString("DB_HOST")};" +
                       $"Port={Env.GetString("DB_PORT")};" +
                       $"Database={Env.GetString("DB_NAME")};" +
                       $"Username={Env.GetString("DB_USER")};" +
                       $"Password={Env.GetString("DB_PASS")}";
builder.Services.AddControllers();

string adminHash = Env.GetString("ADMIN_PASSWORD");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseNpgsql(connectionString));
builder.Services.AddHealthChecks()
	.AddNpgSql(builder.Configuration.GetConnectionString("DefaultConnection")!);
var app = builder.Build();

app.MapHealthChecks("/health");


if (app.Environment.IsDevelopment()){
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
