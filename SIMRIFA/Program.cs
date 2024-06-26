using MudBlazor.Services;
using SIMRIFA.Data;
using SIMRIFA.DataAccess.ConfiguracionRepositorio;
using SIMRIFA.Service.ConfiguracionServicio;
using SIMRIFA.DataAccess.Db_Context;
using Microsoft.EntityFrameworkCore;
using SIMRIFA.Logic.ConfiguracionLogica;
using SIMRIFA.Tools.ConexionFrontBackend;
using SIMRIFA.Tools.Autenticacion;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSingleton<ProteccionDato>();
builder.Services.AddMudServices();
builder.Services.AddHttpClient();

// esta es la interfaz de localStorage
builder.Services.AddTransient<ILocalStorage,  LocalStorage>();


builder.Services.AddDbContext<SIMRIFAdbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sqlOptions =>
	{
	});
}, ServiceLifetime.Transient);

builder.Services.AddScoped<MenuState>();


#region Logica servicio

builder.Services.AddLogica();
#endregion

#region "Patron Repositorio" 
builder.Services.AddRepositorios(configuration);

#endregion     
#region "Servicios" 
builder.Services.AddService();

#endregion

builder.Services.AddScoped(typeof(IConexionApi<,>), typeof(ConexionApi<,>));


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
