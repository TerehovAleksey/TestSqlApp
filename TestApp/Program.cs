using Microsoft.EntityFrameworkCore;
using TestApp.Data;
using TestApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextFactory<AppDb1Context>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Db1Connection")));
builder.Services.AddDbContextFactory<AppDb2Context>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Db2Connection")));
builder.Services.AddScoped<IDb1Service, Db1Service>();
builder.Services.AddScoped<IDb2Service, Db2Service>();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
