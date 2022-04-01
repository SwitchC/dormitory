using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using dormitory;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<dormitoryContext>(option=>option.UseSqlServer
(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication("Cookies")
    .AddCookie(options=>
    {
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/accessdenied";
    });

builder.Services.AddAuthorization();

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

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapGet("/accessdenied", async(HttpContext context)=>
{
    await context.Response.WriteAsync("Access Denied");
    context.Response.StatusCode = 403;
});

app.MapGet("/login", async(HttpContext context)=>
{
    context.Response.ContentType = "text/html; charset=utf-8";
    await context.Response.SendFileAsync("wwwroot/LoginForm.html");
});

app.MapPost("/login",async(string? returnUrl,HttpContext context,dormitoryContext _context)=>
{
    var form = context.Request.Form;
    if (!form.ContainsKey("id") || !form.ContainsKey("password"))
        return Results.BadRequest("id або/і не встановлені");
    string id = form["id"];
    string password = form["password"];
    Student? student = _context.Students.FirstOrDefault(s =>s.Id.ToString() ==id && s.Password == password);
    Employee? employee = _context.Employees.FirstOrDefault(e => e.Id.ToString() == id && e.Pasword == password);
    if (student is null && employee is null) return Results.Unauthorized();
    if (employee != null)
    {
        var claims = new List<Claim>
        {
        new Claim(ClaimsIdentity.DefaultNameClaimType, employee.Id.ToString()),
        new Claim(ClaimsIdentity.DefaultRoleClaimType,"employee")
        };
        var claimsIdentidy = new ClaimsIdentity(claims, "Cookies");
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentidy);
        await context.SignInAsync(claimsPrincipal);
    }
    if (student != null)
    {
        var claims = new List<Claim>
        {
        new Claim(ClaimsIdentity.DefaultNameClaimType, student.Id.ToString()),
        new Claim(ClaimsIdentity.DefaultRoleClaimType,"student")
        };
        var claimsIdentidy = new ClaimsIdentity(claims, "Cookies");
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentidy);
        await context.SignInAsync(claimsPrincipal);
    }
    return Results.Redirect(returnUrl ?? "/");
});

app.MapGet("/logout", async (HttpContext context) =>
{
    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dormitories}/{action=Index}/{id?}");

app.Run();