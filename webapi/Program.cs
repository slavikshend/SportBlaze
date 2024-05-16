using Microsoft.AspNetCore.Authentication.JwtBearer;
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
using AutoMapper;
using webapi.BLL.Helpers;

internal class Program
{
    private static void Main(string[] args)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddAutoMapper(typeof(Program));
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

        builder.Services.AddScoped<ILogReportRepo, LogReportRepo>();
        builder.Services.AddScoped<ILogReportService, LogReportService>();
        builder.Services.AddScoped<IOrderService, OrderService>();
        builder.Services.AddScoped<IOrderRepo, OrderRepo>();
        builder.Services.AddScoped<IFeedbackRepo, FeedbackRepo>();
        builder.Services.AddScoped<IFeedBackService, FeedbackService>();
        builder.Services.AddScoped<IFavouritesService, FavouritesService>();
        builder.Services.AddScoped<IFavouritesRepo, FavouritesRepo>();
        builder.Services.AddScoped<IProductRepo, ProductRepo>();
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<IUserRepo, UserRepo>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<ICRUDRepo<SubCategory>, SubCategoryRepo>();
        builder.Services.AddScoped<ICRUDService<SubCategoryModel>, SubCategoryService>();
        builder.Services.AddScoped<ICRUDRepo<Brand>, BrandRepo>();
        builder.Services.AddScoped<ICRUDService<BrandModel>, BrandService>();
        builder.Services.AddScoped<ICRUDRepo<Category>, CategoryRepo>();
        builder.Services.AddScoped<ICRUDService<CategoryModel>, CategoryService>();
        builder.Services.AddScoped<RecommendationSystem>();
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
        app.UseCors("AllowAngularApp");
        app.UseAuthentication();
        app.UseAuthorization();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.MapControllers();
        app.UseHttpsRedirection();
        app.Run();
    }
}