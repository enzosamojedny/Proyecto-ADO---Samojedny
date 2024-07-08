using Capa_de_Datos.Settings;
using Capa_de_Datos.Repository;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace Proyecto_ADO
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllersWithViews();
            builder.Services.Configure<Capa_de_Datos.Settings.DbConnection>(builder.Configuration.GetSection("ConectionSettings"));
            builder.Services.AddScoped<IEmpleadoRepository, EmpleadoRepository>();
            builder.Services.AddScoped<IEmpleadosRepository, EmpleadosRepository>();
            builder.Services.AddScoped<ICreateEmpleadoRepository, CreateEmpleadoRepository>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
