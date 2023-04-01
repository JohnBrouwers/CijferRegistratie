using CijferRegistratie.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionstring = builder.Configuration.GetConnectionString("CijferRegistratieDbConnection");

// Add services to the container.

builder.Services.AddDbContext<CijferRegistratieDbContext>(options => {
    options.UseSqlServer(connectionstring);
});

builder.Services.AddControllersWithViews();
builder.Services.AddSession(options => { options.Cookie.Name = "Mutaties"; });

builder.Services.AddHttpClient();
builder.Services.AddSingleton(() => new HttpClient(
    new SocketsHttpHandler { PooledConnectionLifetime = TimeSpan.FromMinutes(2) }
));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
