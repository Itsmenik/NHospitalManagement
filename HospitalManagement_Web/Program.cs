using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Hospital.Repositery;
using Hospital.Repositery.Implementation;
using Hospital.Repositery.Interface;
using Hospital.Model;

using Microsoft.AspNetCore.Identity.UI.Services;
using Hopital.Uitility;
using Hospital.Services;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add DbContext with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity services
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Register additional services
builder.Services.AddScoped<IDefaultUserIntializer, DefaultUserIntializer>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailSender, EmailSender>();  
builder.Services.AddTransient<IHospitalInfo, HospitalInofServices>();
builder.Services.AddRazorPages();

var app = builder.Build(); 


// Call DataSedding to initialize the default users and roles
DataSedding();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication(); // Make sure this is included
app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{Area=admin}/{controller=Hospital}/{action=Index}/{id?}");

app.Run();

// Method to seed data
void DataSedding()
{
    
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDefaultUserIntializer>();
        dbInitializer.Initialize();
    }
}

