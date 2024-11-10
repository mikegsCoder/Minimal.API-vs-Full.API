var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();