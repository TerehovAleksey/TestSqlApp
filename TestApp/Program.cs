using Microsoft.EntityFrameworkCore;
using TestApp.Data;
using TestApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextFactory<AppDb1Context>(opt => 
    opt.UseSqlite("Data Source = DB1.db"));
builder.Services.AddDbContextFactory<AppDb2Context>(opt => 
    opt.UseSqlite("Data Source = DB2.db"));

builder.Services.AddScoped<IProductService, ProductService>();
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
