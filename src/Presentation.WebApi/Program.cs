using Microsoft.AspNetCore.Mvc.Controllers;
using NetCoreManualDI.WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

builder.Services
    .AddSingleton<IControllerActivator>(sp => new CustomControllerActivator(sp))
    .AddControllers();

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();

var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger().UseSwaggerUI();
//}

app.MapControllers();

app.Run();
