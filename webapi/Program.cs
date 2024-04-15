using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using webapi.BLL.Models;
using webapi.BLL.Repos.Implementations;
using webapi.BLL.Repos.Interfaces;
using webapi.BLL.Services.Implementations;
using webapi.BLL.Services.Interfaces;
using webapi.DAL.Context;
using webapi.DAL.Entities.Main;

internal class Program
{
    private static void Main(string[] args)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddDbContext<SportsShopDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddSwaggerGen();
        var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
                };
            });
        builder.Services.AddSingleton(jwtSettings);
        builder.Services.AddScoped<IUserRepo, UserRepo>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<ICRUDRepo<Brand>, BrandRepo>();
        builder.Services.AddScoped<ICRUDRepo<Category>, CategoryRepo>();
        builder.Services.AddScoped<ICRUDRepo<Product>, ProductRepo>();
        builder.Services.AddScoped<ICRUDRepo<SubCategory>, SubCategoryRepo>();
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAngularApp",
                builder =>
                {
                    builder.WithOrigins("https://localhost:4200")
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials();
                });
        });


        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            options.OperationFilter<SecurityRequirementsOperationFilter>();
        });

        var app = builder.Build();
        app.UseAuthentication();
        app.UseAuthorization();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
        app.UseCors("AllowAngularApp");

        app.Run();
    }
}