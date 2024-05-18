using SIMRIFA.Api.Controllers;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddTransient<EventApiWompiController>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.Run();

app.MapGet("/EventoWompi", (EventApiWompiController evento) =>
		evento.Index("hola")
);

