using QuestPDF.Infrastructure;
using StatisticsService.API.Middlewares;

namespace StatisticsService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //builder.AddServiceDefaults();

            builder.Services.AddApplicationConfigurations(builder.Configuration);

            var app = builder.Build();

            //app.MapDefaultEndpoints();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseHttpsRedirection();
            app.MapControllers();

            QuestPDF.Settings.License = LicenseType.Community;

            app.Run();
        }
    }
}