
using MinhaApi.Repositories;
using MinhaApi.Services;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine(BCrypt.Net.BCrypt.HashPassword("123456"));


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

// Configuração do CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Registra os repositórios existentes
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
    builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();

// Registra os serviços existentes
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

    builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IFotoService, FotoService>();  // ✨
// IProdutoRepository e IProdutoService já existiam
// Configuração do JWT
var jwtKey = builder.Configuration["Jwt:Key"]
    ?? throw new InvalidOperationException(
        "A chave JWT não foi configurada no appsettings.json.");

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,

                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtKey)),

                ValidateIssuer = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],

                ValidateAudience = true,
                ValidAudience = builder.Configuration["Jwt:Audience"],

                ValidateLifetime = true
            };
    });

// Habilita a autorização
builder.Services.AddAuthorization();

// Injeção de dependência da autenticação
// Adicione estas duas linhas somente se as classes e interfaces existirem.
builder.Services.AddScoped<
    IUsuarioRepository,
    UsuarioRepository>();

builder.Services.AddScoped<
    IAuthService,
    AuthService>();

var app = builder.Build();

//Console.WriteLine(BCrypt.Net.BCrypt.HashPassword("123456"));
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Pipeline da aplicação
app.UseCors("FrontendPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

// ✅ CORRETO: UseCors ANTES de UseStaticFiles
app.UseCors("AllowAll");        // ← já existia
app.UseStaticFiles();           // ← NOVO — libera wwwroot
app.UseAuthentication();        // ← já existia
app.UseAuthorization();         // ← já existia

await api.post('/produtos', {
  nome: 'Notebook',
  preco: 2500
})