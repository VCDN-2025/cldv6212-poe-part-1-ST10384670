using ABCretailpoe1.Services;
using Azure.Storage.Blobs;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton(new BlobServiceClient(
    builder.Configuration.GetConnectionString("AzureBlobStorage")));


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

builder.Services.AddSingleton<BlobStorage>(sp =>
{
    var connectionString = configuration.GetConnectionString("AzureBlobStorage");
    if (string.IsNullOrEmpty(connectionString))
    {
        throw new InvalidOperationException("AzureBlobStorage connection string is not configured. Please check your appsettings.json.");
    }
    return new BlobStorage(connectionString, "product-images"); // "product-images" is your blob container name
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
