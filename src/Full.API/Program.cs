using Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApiDbContext>();

// Add serices:
builder.Services.AddApplicationServices();

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();