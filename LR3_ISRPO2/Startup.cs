using LR3_ISRPO2.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LR3_ISRPO2
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // устанавливаем контекст данных
            services.AddDbContext<GoodsContext>(options =>
           options.UseSqlServer(SqlConnectionIntegratedSecurity));
            services.AddControllers(); // используем контроллеры без представлений
        }
        public static string SqlConnectionIntegratedSecurity
        {
            get
            {
                var sb = new SqlConnectionStringBuilder
                {
                    DataSource = "localhost",
                    // ѕодключение будет с проверкой подлинности пользовател€ Windows
                    IntegratedSecurity = true,
                    // Ќазвание целевой базы данных.
                    InitialCatalog = "sport_pit"
                };
                return sb.ConnectionString;
            }
        }
        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();

            app.UseRouting();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // подключаем маршрутизацию на контроллеры
            });
        }
    }
}