using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MudBlazor.Services;
using SIMRIFA.Web.Data;
using SIMRIFA.DataAccess.Repository;
using SIMRIFA.Service.Tools;
using SIMRIFA.DataAccess.ConfiguracionRepositorio;
using SIMRIFA.Service.ConfiguracionServicio;
using SIMRIFA.DataAccess.Db_Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddMudServices();
builder.Services.AddHttpClient();

builder.Services.AddDbContext<SIMRIFAdbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sqlOptions =>
	{
	});
}, ServiceLifetime.Transient);



#region "Patron Repositorio" 
builder.Services.AddRepositorios(configuration);
#endregion

#region "Servicios" 
builder.Services.AddLogica();
#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
