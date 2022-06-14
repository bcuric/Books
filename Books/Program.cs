using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Books.Data;
using Books.Services.Implementation;
using Books.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BooksContext>(options =>
    options.UseInMemoryDatabase("BooksDb"));

builder.Services.AddTransient<IBookRepository, BookRepository>();
builder.Services.AddTransient<IBookService, BookService>();
builder.Services.AddTransient<IInitaialDataLoad, InitialLoadXml>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//INITIAL DATA LOAD
using (ServiceProvider serviceProvider = builder.Services.BuildServiceProvider())
{
    var initialLoad = serviceProvider.GetRequiredService<IInitaialDataLoad>();
    initialLoad.InitaialData();
}
//INITIAL DATA LOAD END

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
