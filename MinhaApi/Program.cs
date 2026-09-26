using MinhaApi.Repositories;
using MinhaApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

// Registra o Repository
builder.Services.AddScoped<
    IProdutoRepository,
    ProdutoRepository>();

builder.Services.AddScoped<
    IClienteRepository,
    ClienteRepository>();

builder.Services.AddScoped<
    IVendasRepository,
    VendasRepository>();

    builder.Services.AddScoped<
    IFornecedorRepository,
    FornecedorRepository>(); 

     builder.Services.AddScoped<
     IDepartamentoRepository,
      DepartamentoRepository>();


// Registra a Service
builder.Services.AddScoped<
    IProdutoService,
    ProdutoService>();

builder.Services.AddScoped<
    IClienteService,
    ClienteService>();

builder.Services.AddScoped<
    IVendasService,
    VendasService>();

builder.Services.AddScoped<
IFornecedorService,
FornecedorService>();

 builder.Services.AddScoped<
 IDepartamentoService,
 DepartamentoService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();