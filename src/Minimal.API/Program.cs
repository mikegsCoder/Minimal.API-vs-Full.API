using Core.Contracts;
using Core.Services;
using Infrastructure.Data;
using Minimal.API.Features;

//var builder = WebApplication.CreateSlimBuilder(args);
var builder = WebApplication.CreateBuilder(args);

// Using with AoT compilation:
// builder.Services.ConfigureHttpJsonOptions(options => options.SerializerOptions.TypeInfoResolverChain.Insert(0, ApiJsonContext.Default));

// Add services into IoC container:
builder.Services.AddDbContext<ApiDbContext>();
builder.Services.AddSingleton<IDbService, DbService>();
builder.Services.AddSingleton<ICalculator, Calculator>();
builder.Services.AddSingleton<ILogWriter, LogWriter>();

// Create app:
var app = builder.Build();

// Simple examples:
app.MapGet("greet", () => "Hello, world!");
// http://localhost:5000/greet -> Hello, world!
app.MapGet("greet/{name}", (string name) => $"Hello, {name}!");
// http://localhost:5000/greet/George -> Hello, George!

// Examples of using calculations synchronous logic:
app.AddCalculations();

// Examples of using calculations asynchronous logic:
app.AddCalculationsAsync();

app.AddInformations();
app.AddDbFeatures();

app.Run();

// Using with AoT compilation:
//[JsonSerializable(typeof(string)), JsonSerializable(typeof(string[])), JsonSerializable(typeof(int)), JsonSerializable(typeof(int[])), JsonSerializable(typeof(bool))]
//[JsonSerializable(typeof(LogRequest))]
//internal partial class ApiJsonContext : JsonSerializerContext;
