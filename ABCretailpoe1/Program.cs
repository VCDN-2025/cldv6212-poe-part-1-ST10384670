using ABCretailpoe1.Services;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure Table Storage
builder.Services.AddSingleton<TableStorage>(sp =>
{
    var connectionString = configuration.GetConnectionString("AzureTableStorage");
    if (string.IsNullOrEmpty(connectionString))
    {
        throw new InvalidOperationException("AzureTableStorage connection string is not configured. Please check your appsettings.json or environment variables.");
    }
    return new TableStorage(connectionString);
});




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
