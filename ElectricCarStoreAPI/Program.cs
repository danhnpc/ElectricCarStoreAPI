using CloudinaryDotNet;
using ElectricCarStore_BLL.Extension;
using ElectricCarStore_BLL.IService;
using ElectricCarStore_BLL.Service;
using ElectricCarStore_DAL.IRepository;
using ElectricCarStore_DAL.Models;
using ElectricCarStore_DAL.Models.QueryModel;
using ElectricCarStore_DAL.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
            .AddJwtBearer(options =>
            {
                var signingKey = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SignKey"]);
                var encryptKey = Convert.FromBase64String(builder.Configuration["Jwt:EncryptKey"]);
                string validIssusers = builder.Configuration["ApiUrl"];
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,  // Tắt tạm thời
                    ValidIssuer = validIssusers,
                    ValidateAudience = false,  // Tắt tạm thời
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(signingKey),
                    TokenDecryptionKey = new SymmetricSecurityKey(encryptKey),
                    ClockSkew = TimeSpan.Zero
                };
            });

builder.Services.AddAuthorization(options =>
{
    var policy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);

    //Require authentication for all users
    policy = policy.RequireAuthenticatedUser();
    options.DefaultPolicy = policy.Build();
});

// Thêm vào cấu hình services
builder.Services.AddOptions<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme)
    .Configure<IConfiguration>((options, configuration) =>
    {
        // Cấu hình khác của bạn...
        options.MapInboundClaims = false; // Thử cài đặt này nếu bạn gặp vấn đề với claims
    });

// Add services to the container.

builder.Services.AddDbContext<ElectricCarStoreContext>((serviceProvider, options) =>
{
    // Gọi hàm ConfigureDbContext từ DBConnection để cấu hình connection string
    DBConnection.ConfigureDbContext(serviceProvider, options, builder.Configuration);
});

builder.Services.AddControllers(options =>
{
    options.RespectBrowserAcceptHeader = true;
    options.OutputFormatters.RemoveType<StringOutputFormatter>();
    options.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();
}).AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000); // L?ng nghe trên t?t c? IP
});
//Dang ki Clounary
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("Cloudinary"));
builder.Services.AddSingleton<Cloudinary>(sp =>
{
    var config = sp.GetRequiredService<IOptions<CloudinarySettings>>().Value;
    Account account = new Account(
        config.CloudName,
        config.ApiKey,
        config.ApiSecret);

    return new Cloudinary(account);
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();
builder.Services.AddScoped<IBannerRepository, BannerRepository>();
builder.Services.AddScoped<INewsRepository, NewsRepository>();
builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<ICarTypeRepository, CarTypeRepository>();

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<AuthService>(); 
builder.Services.AddTransient<IImageService, CloudinaryImageService>();
builder.Services.AddTransient<IBannerService, BannerService>();
builder.Services.AddTransient<INewsService, NewsService>();
builder.Services.AddTransient<IContactService, ContactService>();
builder.Services.AddTransient<ICarService, CarService>();
builder.Services.AddTransient<ICarTypeService, CarTypeService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ElectricCarStore", Version = "v1" });

    // Cấu hình xác thực Swagger ?? thêm nhập token
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter your token here",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
             new OpenApiSecurityScheme
             {
                  Reference = new OpenApiReference
                  {
                       Type = ReferenceType.SecurityScheme,
                       Id = "Bearer"
                  }
             },
             Array.Empty<string>()
        }
    });
});

var app = builder.Build();
app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsLocal())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ElectricCarStore"));
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
