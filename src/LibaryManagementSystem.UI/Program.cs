using LibaryManagementSystem.Data.Context;
using LibaryManagementSystem.Data.Repositories;
using LibaryManagementSystem.Data.Repositories.Abstracts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddDbContext<PostgreDbContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("PostgreDBConnection")));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDbContext, PostgreDbContext>();

builder.Services.AddAuthentication("CookieAuthentication")
    .AddCookie("CookieAuthentication", config =>
    {
        config.Cookie.Name = "UserLoginCookie"; 
        config.LoginPath = "/Auth/Login";
        config.Events = new CookieAuthenticationEvents
        {
            OnRedirectToLogin = context =>
            {
                var loginUrl = "/Auth/Login?isError=1"; 
                context.Response.Redirect(loginUrl);
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddControllersWithViews().AddNToastNotifyToastr(new ToastrOptions()
{
    ProgressBar = true,
    PositionClass = ToastPositions.BottomRight,
    PreventDuplicates = true,
    CloseButton = true
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseNToastNotify();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}");

app.Run();
