using ClinicaAmanecer.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Conexión a SQL Server (AppDBContext)
var connectionString = builder.Configuration.GetConnectionString("ClinicaAmanecerConnectionString");
builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();