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
            // ������������� �������� ������
            services.AddDbContext<GoodsContext>(options =>
           options.UseSqlServer(SqlConnectionIntegratedSecurity));
            services.AddControllers(); // ���������� ����������� ��� �������������
        }
        public static string SqlConnectionIntegratedSecurity
        {
            get
            {
                var sb = new SqlConnectionStringBuilder
                {
                    DataSource = "DESKTOP-ON4LKTV\\SQLEXPRESS",
                    // ����������� ����� � ��������� ����������� ������������ Windows
                    IntegratedSecurity = true,
                    // �������� ������� ���� ������.
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
                endpoints.MapControllers(); // ���������� ������������� �� �����������
            });
        }
    }
}