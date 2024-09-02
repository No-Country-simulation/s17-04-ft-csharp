using JuniorHub.API;

var builder = WebApplication.CreateBuilder(args);

var app = builder
    .ConfigureServices()
    .ConfigurePipeline();

app.MapGet("/", () => "Junior Hub");

app.Run();
