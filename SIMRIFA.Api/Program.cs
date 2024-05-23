using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SIMRIFA.Api.Controllers;
using SIMRIFA.DataAccess.Db_Context;
using SIMRIFA.DataAccess.ConfiguracionRepositorio;
using SIMRIFA.Service.ConfiguracionServicio;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

builder.Services.AddTransient<EventApiWompiController>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpClient();
builder.Services.AddSwaggerGen();


builder.Services.AddControllers().AddJsonOptions(options =>
{
	options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});


#region DbContext SQL Server
builder.Services.AddDbContext<SIMRIFAdbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sqlOptions =>
	{
	});
});

#endregion

#region "Patron Repositorio" 
builder.Services.AddRepositorios(configuration);
#endregion

#region "Servicios" 
builder.Services.AddLogica();
#endregion

builder.Services.AddSignalR(e =>
{
	e.MaximumReceiveMessageSize = 102400000;
	e.EnableDetailedErrors = true;
});


builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAll", builder =>
	{
		builder.AllowAnyOrigin()
			   .AllowAnyMethod()
			   .AllowAnyHeader();
	});
});





var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
	app.UseSwagger();
	app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1"));
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.UseHttpsRedirection();
app.UseCors();
app.UseCors("AllowOrigin");


await app.RunAsync();

