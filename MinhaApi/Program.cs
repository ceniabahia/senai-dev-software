using MinhaApi.Repositories;
using MinhaApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ Registra o Repository
builder.Services.AddScoped<
    IProdutoRepository,
    ProdutoRepository>();

// ✅ Registra a Service
builder.Services.AddScoped<
    IProdutoService,
    ProdutoService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();