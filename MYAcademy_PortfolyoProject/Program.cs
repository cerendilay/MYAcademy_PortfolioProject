using MYAcademy_PortfolyoProject.Context;
using MYAcademy_PortfolyoProject.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<PortfolyoContext>();
builder.Services.AddScoped<ImageUploadService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
// MapStaticAssets yalnızca derleme anında var olan dosyaları sunar;
// Admin panelinden sonradan yüklenen görseller (wwwroot/uploads) için UseStaticFiles gerekir.
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=AdminLayout}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();