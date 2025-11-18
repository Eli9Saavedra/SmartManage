using Microsoft.EntityFrameworkCore;
using SmartManage.Components;
using SmartManage.Components.Data;
using SmartManage.Components.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<SmartManageContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SmartManageDb")));

// application services
builder.Services.AddScoped<RecordService>();
builder.Services.AddScoped<CurrentUserService>();
builder.Services.AddScoped<AuthService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
