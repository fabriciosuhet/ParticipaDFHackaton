using SolucaoParticipaDF.API.Infra.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();
builder.Services.AddSwaggerConfig();

// Swagger
builder.Services.AddEndpointsApiExplorer();


// Seus services
builder.Services.AddServices();

var app = builder.Build();

// Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
