using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SIMRIFA.Api.Controllers;
using SIMRIFA.DataAccess.Db_Context;
using SIMRIFA.DataAccess.ConfiguracionRepositorio;
using SIMRIFA.Service.ConfiguracionServicio;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SIMRIFA.Logic.ConfiguracionLogica;
using Microsoft.Extensions.Caching.Distributed;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSignalR(e =>
{
	e.MaximumReceiveMessageSize = 102400000;
	e.EnableDetailedErrors = true;
});

builder.Services.AddSwaggerGen(options =>
{
	options.SwaggerDoc("v1", new OpenApiInfo
	{
		Version = "v1",
		Title = "Sistema de sorteos API",
		Description = "Servicios para uso exclusivo de OsdagupO.",

	});
	options.CustomSchemaIds(type => type.ToString());
	options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Name = "Authorization",
		Description = "Bearer Authentication with JWT Token",
		Type = SecuritySchemeType.Http
	});

	options.AddSecurityRequirement(new OpenApiSecurityRequirement()
		{
			{
				new OpenApiSecurityScheme
				{
					Reference = new OpenApiReference
					{
						Id = "Bearer",
						Type = ReferenceType.SecurityScheme
					},
				},
				new List<string>()
			}
		});

});


builder.Services.AddControllers().AddJsonOptions(options =>
{
	options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
	options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
	options.JsonSerializerOptions.PropertyNamingPolicy = null;
});


builder.Services.AddCors(options =>
{

	options.AddPolicy("https://localhost:7297/", builder =>
	{
		builder.AllowAnyOrigin()
			   .AllowAnyMethod()
			   .AllowAnyHeader();
	});
	options.AddPolicy("AllowOrigin", option => option.AllowAnyOrigin());
});

builder.Services.AddMemoryCache();


#region Auten

builder.Services.AddHttpContextAccessor().AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(x =>
	   {
		   x.TokenValidationParameters = new TokenValidationParameters()
		   {
			   ValidateActor = true,
			   ValidateAudience = true,
			   ValidateLifetime = true,
			   ValidateIssuerSigningKey = true,
			   ValidIssuer = configuration["Jwt:Issuer"],
			   ValidAudience = configuration["Jwt:Audience"],
			   IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Jwt:Key"])),
		   };
	   });


builder.Services.AddHttpClient();
#endregion

#region DbContext SQL Server
builder.Services.AddDbContext<SIMRIFAdbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sqlOptions =>
	{
	});

});

#endregion

#region

builder.Services.AddLogica();

#endregion

#region "Patron Repositorio" 
builder.Services.AddRepositorios(configuration);
#endregion

#region "Servicios" 
builder.Services.AddService();
#endregion




var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();



app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors();
app.UseCors("AllowOrigin");
app.MapControllers();
app.UseStaticFiles();









await app.RunAsync();

