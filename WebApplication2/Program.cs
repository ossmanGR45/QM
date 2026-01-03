using Microsoft.EntityFrameworkCore;
using QM.DataAccess.Data;

using QM.DataAccess.Repo;
using QM.DataAccess.Repo.IRepo;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using QM.Models.DataModels;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>options.UseSqlServer( builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<User>()
    .AddRoles<IdentityRole<int>>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience= true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddAuthorization();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });




var app = builder.Build();




// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowReactApp");

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=initi}/{controller=Form}/{action=Index}/{id?}")
    .WithStaticAssets();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<User>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>();

    var email = "AbedAlrahman@gmail.com";
    var password = "Abed@12345"; // Ensure this meets complexity requirements
    var roleName = "Admin";

    // 1. Ensure the Role exists
    if (!await roleManager.RoleExistsAsync(roleName))
    {
        await roleManager.CreateAsync(new IdentityRole<int>(roleName));
    }

    // 2. Create the User
    var user = await userManager.FindByEmailAsync(email);
    if (user == null)
    {
        user = new User
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, roleName);
        }
    }
}


app.Run();



