using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MVC18.Data;
using MVC18.Services.Implementations.Auth;
using MVC18.Services.Implementations.Commons;
using MVC18.Services.Implementations.Products;
using MVC18.Services.Implementations.Users.Customers;
using MVC18.Services.Implementations.Users.Employees;
using MVC18.Services.Interfaces.Auth;
using MVC18.Services.Interfaces.Commons;
using MVC18.Services.Interfaces.Products;
using MVC18.Services.Interfaces.Users.Customers;
using MVC18.Services.Interfaces.Users.Employees;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Auto Mapper
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddMaps(typeof(Program).Assembly);
});

// Memory Cache (for OTP, email verification, etc)
builder.Services.AddMemoryCache();

//Session Section
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".NetCore.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

//Auth Section
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies["jwt"];
                return Task.CompletedTask;
            },
            OnChallenge = context =>
            {
                context.HandleResponse();
                context.Response.Redirect("/Auth/Login");
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Customer", policy => policy.RequireClaim(System.Security.Claims.ClaimTypes.Role, "Customer"));
    options.AddPolicy("Manager", policy => policy.RequireAssertion(context =>
    {
        var roleClaim = context.User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
        return roleClaim != null && roleClaim != "Customer";
    }));
});

//Connection String
var myConnectionString = builder.Configuration.GetConnectionString("MyConnectString");
builder.Services.AddDbContext<LaptopWebDb06Context>(option => option.UseSqlServer(myConnectionString));

//Service Scope
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ILaptopService, LaptopService>();
builder.Services.AddScoped<ICpuService, CpuService>();
builder.Services.AddScoped<IGpuService, GpuService>();
builder.Services.AddScoped<IRamService, RamService>();
builder.Services.AddScoped<IStorageService, StorageService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<ICommonService, CommonService>();


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

app.UseSession();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
