using ElectricCarStore_BLL.Extension;
using ElectricCarStore_BLL.IService;
using ElectricCarStore_BLL.Service;
using ElectricCarStore_DAL.IRepository;
using ElectricCarStore_DAL.Models;
using ElectricCarStore_DAL.Repository;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});
// Add services to the container.

builder.Services.AddDbContext<ElectricCarStoreContext>((serviceProvider, options) =>
{
    // Gọi hàm ConfigureDbContext từ DBConnection để cấu hình connection string
    DBConnection.ConfigureDbContext(serviceProvider, options, builder.Configuration);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000); // L?ng nghe trên t?t c? IP
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserService, UserService>(); // nếu bạn có service riêng cho User

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsLocal())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
