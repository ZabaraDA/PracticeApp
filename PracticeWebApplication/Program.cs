using PracticeWebApplication.Services;
using System.Text.Json.Serialization;

namespace PracticeWebApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers().AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            int maxLimit = builder.Configuration.GetSection("Settings").GetSection("ParallelLimit").Get<int>();
            builder.Services.AddSingleton(new ParallelLimit(maxLimit));

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();


            app.Run();
        }
    }
}