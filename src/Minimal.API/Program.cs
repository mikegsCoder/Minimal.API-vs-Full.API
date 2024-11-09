using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using System.Threading;
using Microsoft.AspNetCore.Mvc;

//var builder = WebApplication.CreateSlimBuilder(args);
var builder = WebApplication.CreateBuilder(args);

// Using with AoT compilation:
// builder.Services.ConfigureHttpJsonOptions(options => options.SerializerOptions.TypeInfoResolverChain.Insert(0, ApiJsonContext.Default));

// Create app:
var app = builder.Build();

// Simple examples:
app.MapGet("greet", () => "Hello, world!");
// http://localhost:5000/greet -> Hello, world!
app.MapGet("greet/{name}", (string name) => $"Hello, {name}!");
// http://localhost:5000/greet/George -> Hello, George!

app.Run();
