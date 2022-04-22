using AdminPanel.Models;
using AdminPanelProject.Business.Abstract;
using AdminPanelProject.Business.Concrete;
using AdminPanelProject.DataAccessLayer.Abstract;
using AdminPanelProject.DataAccessLayer.Concrete;
using AdminPanelProject.Helpers;
using AdminPanelProject.Helpers.Abstract;
using AdminPanelProject.Helpers.UserHelpers.Abstract;
using AdminPanelProject.Helpers.UserHelpers.Concrete;
using AdminPanelProject.Services.Abstract;
using AdminPanelProject.Services.Concrete;
using Core.Authentication.Abstract;
using Core.Authentication.Concrete;
using Core.Context.EFContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var appSettings = builder.Configuration.GetSection("AppSettings").Get<JwtSettings>();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettings.Secret)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
builder.Services.AddScoped<IdentityRole, UserRole>();
builder.Services.AddScoped<IdentityUser, AppUser>();
builder.Services.AddScoped<UserManager<AppUser>>();
builder.Services.AddScoped<RoleManager<UserRole>>();
builder.Services.AddScoped<IRoleHelpers, RoleHelpers>();
builder.Services.AddScoped<IUserHelpers, UserHelpers>();
builder.Services.AddScoped<ICompanyDal, CompanyRepository>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<ICompanyManager, CompanyManager>();
builder.Services.AddScoped<IJwtAuthentication, JwtAuthentication>();


builder.Services.AddDbContext<IdentityContext>(opt =>
{
    opt.UseSqlServer(appSettings.ConnectionName);
});

builder.Services.AddIdentity<AppUser, UserRole>(opt =>
{
    opt.Lockout.MaxFailedAccessAttempts = 5;
    opt.Lockout.AllowedForNewUsers = true;
    opt.Password.RequiredLength = 6;

}).AddEntityFrameworkStores<IdentityContext>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(opt =>
{
    opt.AllowAnyHeader();
    opt.AllowAnyMethod();
    opt.AllowAnyOrigin();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
