using DBL;
using TuneBlazorApp.Components;
using Microsoft.AspNetCore.StaticFiles;
using System.IO;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddControllers();
builder.Services.AddSingleton<SongDB>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Only need this once
app.UseRouting();
app.UseAuthorization();
app.UseAntiforgery();

// Custom endpoint for serving uploaded files with better control
app.MapGet("/api/audio/{id}/{filename}", (string id, string filename) =>
{
    var path = Path.Combine(app.Environment.WebRootPath, "uploads", id, filename);

    if (!File.Exists(path))
    {
        return Results.NotFound();
    }

    // Use FileExtensionContentTypeProvider to determine MIME type
    var provider = new FileExtensionContentTypeProvider();
    if (!provider.TryGetContentType(filename, out var contentType))
    {
        contentType = "application/octet-stream";
    }

    return Results.File(path, contentType);
});

app.MapControllers();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();