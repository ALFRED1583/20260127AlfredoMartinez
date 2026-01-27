using PruebaBSCI.DAL;
using PruebaBSCI.BL;
using Microsoft.OpenApi.Models;
using System.Reflection;
using PruebaBSCI.Utilidades;

var builder = WebApplication.CreateBuilder(args);
var cn = builder.Configuration.GetConnectionString("BSCI");
builder.Services.AddSingleton(new IncidenciaDAL(cn));
builder.Services.AddSingleton(new CategoriaDAL(cn));
builder.Services.AddScoped<IncidenciaBL>();
builder.Services.AddScoped<CategoriaBL>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Prueba BSCI API", Description = "API para prueba Programador .NET", Version = "v1" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Prueba BSCI - API V1");
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ControladorExepciones>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
