using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SIMRIFA.Api.Controllers;
using SIMRIFA.DataAccess.Db_Context;
using SIMRIFA.DataAccess.ConfiguracionRepositorio;
using SIMRIFA.Service.ConfiguracionServicio;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

builder.Services.AddTransient<EventApiWompiController>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpClient();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR(e =>
{
	e.MaximumReceiveMessageSize = 102400000;
	e.EnableDetailedErrors = true;
});

//builder.Services.AddSwaggerGen(options =>
//{
//	options.SwaggerDoc("v1", new OpenApiInfo
//	{
//		Version = "v1",
//		Title = "Sistema rifas TechsMinds",
//		Description = "Servicios para uso exclusivo de TechsMinds",

//	});
//	options.CustomSchemaIds(type => type.ToString());

//});

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAll", builder =>
	{
		builder.AllowAnyOrigin()
			   .AllowAnyMethod()
			   .AllowAnyHeader();
	});
});



#region DbContext SQL Server
builder.Services.AddDbContext<SIMRIFAdbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sqlOptions =>
	{
	});
}, ServiceLifetime.Transient);

#endregion

#region "Patron Repositorio" 
builder.Services.AddRepositorios(configuration);
#endregion

#region "Servicios" 
builder.Services.AddLogica();
#endregion


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

