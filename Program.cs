using CompensationWebApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace CompensationWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            

            builder.Services.AddControllers()
                .AddJsonOptions(op =>
                {
                    op.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                })
                ;


            builder.Services.AddDbContext<ApplicationDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });



            var app = builder.Build();

           
            if (app.Environment.IsDevelopment())
            {

            }
            app.UseExceptionHandler("/error");

            app.MapGet("/error", () =>
            {
                return Results.Problem();
            });


            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
