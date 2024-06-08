using Dynamo.Ui.Web.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Dynamo.Ui.Web.Data;
using Microsoft.AspNetCore.Identity;

namespace Dynamo.Ui.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<DynamoUiWebContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DynamoUiWebContext") ?? throw new InvalidOperationException("Connection string 'DynamoUiWebContext' not found.")));

            builder.Services.AddQuickGridEntityFrameworkAdapter();;

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
