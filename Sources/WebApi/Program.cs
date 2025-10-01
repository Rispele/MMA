using Sources.ServiceDefaults;
using WebApi.Services.Implementations;
using WebApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.AddServiceDefaults();

var serviceCollection = builder.Services;

//OpenApi
serviceCollection.AddOpenApi();
serviceCollection.AddEndpointsApiExplorer();

serviceCollection.AddControllers();

serviceCollection.AddSingleton<IRoomService, RoomService>();
serviceCollection.AddSingleton<IFileService, FileService>();

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

app.MapControllers();
app.UseHttpsRedirection();

app.Run();