using CoreShelf.API.Middleware;
using CoreShelf.Core.Entities;
using CoreShelf.Core.Interfaces;
using CoreShelf.Infrastructure.Data;
using CoreShelf.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace CoreShelf.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers();
            builder.Services.AddDbContext<CoreShelfDbContext>(x =>
            {
                x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddSingleton<IConnectionMultiplexer>(config =>
            {
                var connString = builder.Configuration.GetConnectionString("Redis") ?? throw new Exception("Cannot get redis connection string");
                var configuration = ConfigurationOptions.Parse(connString, true);
                return ConnectionMultiplexer.Connect(configuration);
            });
            builder.Services.AddSingleton<ICartService, CartService>();
            builder.Services.AddAuthorization();
            builder.Services.AddIdentityApiEndpoints<AppUser>()
                .AddEntityFrameworkStores<CoreShelfDbContext>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseMiddleware<ExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            app.MapGroup("api").MapIdentityApi<AppUser>();

            app.Run();
        }
    }
}
