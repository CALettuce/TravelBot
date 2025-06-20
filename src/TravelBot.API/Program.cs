using Microsoft.EntityFrameworkCore;
using TravelBot.Application.Interfaces;
using TravelBot.Application.Services;
using TravelBot.Application.Settings;
using TravelBot.Infrastructure;
using TravelBot.Infrastructure.External.OpenAI;
using TravelBot.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<OpenAISettings>(builder.Configuration.GetSection("OpenAI"));
builder.Services.AddScoped<IClimaService, ClimaService>();
builder.Services.AddHttpClient<IOpenAIService, OpenAIService>();
builder.Services.AddSingleton<IChatMemoryService, ChatMemoryService>();
builder.Services.AddDbContext<TravelBotDbContext>(options =>
    options.UseSqlite("Data Source=travelbot.db"));
builder.Services.AddScoped<IChatStorageService, ChatStorageService>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
