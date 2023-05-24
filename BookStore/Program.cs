using BookStore.Models.Repositories;
using BookStore.Models;
namespace BookStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddMvc();
            builder.Services.AddSingleton<IBookStoreRepository<Author>, AuthorRepository>();
            builder.Services.AddSingleton<IBookStoreRepository<Book>, BookRepository>();

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

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Author}/{action=Index}");
            });
            app.UseAuthorization();

            app.MapRazorPages();

            
            app.Run();
        }
    }
}