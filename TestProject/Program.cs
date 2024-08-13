using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TestProject.Db;
using TestProject.Infrastracture.Handlers;
using TestProject.Mapping;
using TestProject.Models;
using TestProject.Services;
using TestProject.Services.Abstractions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddDbContext<NodesDbContext>();

builder.Services.AddExceptionHandler<NodesExceptionHandler>();

builder.Services.AddTransient<INodesService, NodesService>();
builder.Services.AddTransient<INodesConverter, NodesConverter>();

builder.Services.AddScoped<IValidator<NodeModel>, NodeModelValidator>();

builder.Services.AddTransient<IExceptionJournalService, ExceptionJournalService>();
builder.Services.AddTransient<IExceptionRecordConverter, ExceptionRecordConverter>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<NodesDbContext>();
    await db.Database.MigrateAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseExceptionHandler(_ => { });

app.MapControllers();

app.Run();
