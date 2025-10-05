using Application;
using Application.Configuration;
using Application.Implementations.Services.DtoConverters;
using Application.Implementations.Services.Files;
using Application.Implementations.Services.Rooms;
using Sources.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder
    .AddServiceDefaults()
    .AddDomainContext();

builder.Services
    .AddScoped<IRoomService, RoomService>()
    .AddScoped<IFileService, FileService>()
    .AddSingleton<RoomDtoConverter>();

builder.Services.AddOpenApi();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(b =>
    {
        b.SwaggerEndpoint("/openapi/v1.json", "My API V1");
    });
}

// app.UseHttpsRedirection();
app.MapControllers();

app.Run();