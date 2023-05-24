global using OnlineStore.Services.EmailService;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.Proxies;
//using MySQL.Data.EntityFrameworkCore.Extensions;

using Microsoft.IdentityModel.Tokens;
using OnlineStore.Models;
using System.Text;
using System.Text.Json.Serialization;

namespace OnlineStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddDbContext<BookstoresdbContext>(options =>
            {
                //the change occurs here.
                //builder.cofiguration and not just configuration

                options.UseMySQL(builder.Configuration.GetConnectionString("WebApiDatabase")).EnableSensitiveDataLogging();
                // <-- enable Lazy Loading
                    
            });
            builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                            .AddJwtBearer(options =>
                            {
                                options.TokenValidationParameters = new TokenValidationParameters
                                {
                                    ValidateIssuerSigningKey = true,
                                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                                                        .GetBytes(builder.Configuration.GetSection("JWT:Secret").Value)),
                                    ValidateAudience = false,
                                    ValidateIssuer = false,

                                };
                            });

          
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();
            app.MapControllers();

            app.Run();
        }
    }
}