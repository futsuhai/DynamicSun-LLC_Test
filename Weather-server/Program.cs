using Microsoft.EntityFrameworkCore;
using Weather_server.Context;
using Weather_server.Mapper;
using Weather_server.Repositories;
using Weather_server.Services.DayService;
using Weather_server.Services.ParserService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Repositories

//Mapper
builder.Services.AddAutoMapper(typeof(AppMappingProfile));
// Options

// Services
builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL")));
builder.Services.AddScoped<IDbRepository, DbRepository>();
builder.Services.AddTransient<IWeatherService, WeatherService>();
builder.Services.AddTransient<IParserService, ParserService>();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "http://localhost:4200");
    });
}
app.UseCors(options =>
{
    options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
});
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();