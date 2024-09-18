using Microsoft.Extensions.FileProviders;
using Npgsql;
using Microsoft.Extensions.FileProviders;
using PersonalInfo.DAO;

namespace PersonalInfo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connection = builder.Configuration.GetConnectionString("PostgresDB");
            builder.Services.AddScoped(provider => new NpgsqlConnection(connection));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IPersonalInfoDao, PersonalInfoImple>();

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(builder.Environment.ContentRootPath, "Images")),
                RequestPath = "/static/images"
            });


            app.MapControllers();

            app.Run();
        }
    }
}
