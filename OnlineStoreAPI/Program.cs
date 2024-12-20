using OnlineStoreAPI.Services;
using OnlineStoreAPI.Storage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ApplicationStorage>();
builder.Services.AddHostedService<StorageSaver>();

var app = builder.Build();

var useSwagger = Environment.GetEnvironmentVariable("USE_SWAGGER");

// Configure the HTTP request pipeline.
if(useSwagger != null || app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();