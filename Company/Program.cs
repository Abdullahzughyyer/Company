using Company.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using Company.ViewModels;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// ����� DbContext
builder.Services.AddDbContext<CompanyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("sqlCon")));

// ����� ����� ��� MVC
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();
var app = builder.Build();

// ����� ��� HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // ������� ������� ����� �������
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // ����� HTTP
}

// ����� ��� Static files (��� CSS� JS� ���)
app.UseStaticFiles();

// ����� ��� Routing ���� MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Department}/{action=Index}/{id?}");


// ����� �������
app.Run();
