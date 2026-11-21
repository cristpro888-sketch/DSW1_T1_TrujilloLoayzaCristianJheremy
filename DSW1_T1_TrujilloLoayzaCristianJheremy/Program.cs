using DSW1_T1_TrujilloLoayzaCristianJheremy.Repositories.Implementations;
using DSW1_T1_TrujilloLoayzaCristianJheremy.Repositories.Interfaces;
using DSW1_T1_TrujilloLoayzaCristianJheremy.Services.Implementations;
using DSW1_T1_TrujilloLoayzaCristianJheremy.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ILibroRepository, LibroRepository>();
builder.Services.AddScoped<ILibroService, LibroService>();

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
    pattern: "{controller=Libro}/{action=Index}/{id?}");

app.Run();
