using App.API.Extension;
using App.Application.Extension;
using App.Bus;
using App.Persistance.Extensions;
using Serilog;

Log.Logger = new LoggerConfiguration()
  .WriteTo.File("Logs/log-txt", rollingInterval: RollingInterval.Day)
  .CreateLogger();
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddExceptionHandlerExt().AddCacheExt();
builder.Services.MediatrJwtEx(builder.Configuration);
builder.Services.AddBuss(builder.Configuration);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddServicesEx(builder.Configuration).AddRepoEx(builder.Configuration);

builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
