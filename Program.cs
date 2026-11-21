using System.Text;
using ClinicaAmanecer.Autorizacion;
using ClinicaAmanecer.Data;
using ClinicaAmanecer.Extenciones;
using ClinicaAmanecer.Models.Settings;
using ClinicaAmanecer.Repositories;
using ClinicaAmanecer.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using static ClinicaAmanecer.Autorizacion.JWTPermisionPolicies;
using static ClinicaAmanecer.Autorizacion.JWTPermisionPolicies.PermissionAuthorizationHandler;

var builder = WebApplication.CreateBuilder(args);

// 1. Controladores con Vistas y Configuración
builder.Services.AddControllersWithViews();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("Smtp"));

var jwtSetting = builder.Configuration.GetSection("Jwt").Get<JwtSettings>()
    ?? throw new InvalidOperationException("No está presente la configuración de Jwt en appsettings.json / secrets.json");

// 2. Base de Datos SQL Server (DB_ClinicaAmanecer)
builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ClinicaAmanecerConnectionString")));

// 3. Inyección de Dependencias: Repositorios
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IRoleRepository, RolRepository>();
builder.Services.AddScoped<IPermissionRepository, PermisoRepository>();
builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
builder.Services.AddScoped<ICitaRepository, CitaRepository>();
builder.Services.AddScoped<IConsultaMedicaRepository, ConsultaMedicaRepository>();
builder.Services.AddScoped<IFacturaRepository, FacturaRepository>();

// 4. Inyección de Dependencias: Servicios
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRolePermisoService, RolePermisoService>();
builder.Services.AddScoped<IPacienteService, PacienteService>();
builder.Services.AddScoped<ICitaService, CitaService>();
builder.Services.AddScoped<IConsultaMedicaService, ConsultaMedicaService>();
builder.Services.AddScoped<IFacturaService, FacturaService>();

// 5. Sesión y Autenticación JWT en Cookies HttpOnly
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                if (context.Request.Cookies.ContainsKey("access_token"))
                {
                    context.Token = context.Request.Cookies["access_token"];
                }
                return Task.CompletedTask;
            },
            OnChallenge = context =>
            {
                context.HandleResponse();
                if (!context.Response.HasStarted)
                {
                    context.Response.Redirect("/Auth/Login");
                }
                return Task.CompletedTask;
            },
            OnForbidden = context =>
            {
                if (!context.Response.HasStarted)
                {
                    context.Response.Redirect("/Auth/AccessDenied");
                }
                return Task.CompletedTask;
            }
        };

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtSetting.Issuer,
            ValidateAudience = true,
            ValidAudience = jwtSetting.Audience,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.Key)),
            ClockSkew = TimeSpan.Zero
        };
    });

// 6. Políticas Dinámicas de Autorización por Permiso
builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();

builder.Services.AddAuthorization();

var app = builder.Build();

// 7. Migración Automática y Sembrado de las 20 Tablas (DbSeeder)
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDBContext>();
    await DbSeeder.SeedAsync(db);
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();