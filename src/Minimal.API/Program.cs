using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using System.Threading;
using Core.Contracts;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

//var builder = WebApplication.CreateSlimBuilder(args);
var builder = WebApplication.CreateBuilder(args);

// Using with AoT compilation:
// builder.Services.ConfigureHttpJsonOptions(options => options.SerializerOptions.TypeInfoResolverChain.Insert(0, ApiJsonContext.Default));

// Add services into IoC container:
builder.Services.AddSingleton<ICalculator, Calculator>();

// Create app:
var app = builder.Build();

// Simple examples:
app.MapGet("greet", () => "Hello, world!");
// http://localhost:5000/greet -> Hello, world!
app.MapGet("greet/{name}", (string name) => $"Hello, {name}!");
// http://localhost:5000/greet/George -> Hello, George!

// Examples of using synchronous logic:
var calcGroup = app.MapGroup("calc");
calcGroup.MapGet("add", ([FromQuery] int a, [FromQuery] int b, [FromServices] ICalculator calculator) => $"{a} + {b} = {calculator.Add(a, b)}");
// http://localhost:5000/calc/add?a=2&b=5 -> 2 + 5 = 7
// http://localhost:5000/calc/add?x=2&y=5 -> Status Code:400 Bad Request

app.Run();

// Using with AoT compilation:
//[JsonSerializable(typeof(string)), JsonSerializable(typeof(string[])), JsonSerializable(typeof(int)), JsonSerializable(typeof(int[])), JsonSerializable(typeof(bool))]
//[JsonSerializable(typeof(LogRequest))]
//internal partial class ApiJsonContext : JsonSerializerContext;
