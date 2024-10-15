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

builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IDefaultUserIntializer, DefaultUserIntializer>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(); 
builder.Services.AddScoped<IHospitalInfo, HospitalInofServices>(); 
builder.Services.AddScoped<IRoomServices, RoomServices>();

builder.Services.AddRazorPages();

var app = builder.Build();

DataSedding();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication(); 
app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Admin}/{controller=Hospital}/{action=Index}/{id?}");

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


