using eiendomsverdi_atomreaktoren.Db;
using eiendomsverdi_atomreaktoren.Interfaces;
using eiendomsverdi_atomreaktoren.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddMemoryCache();
builder.Services.AddDbContextFactory<DataContext>();
builder.Services.AddSingleton<IValveControl, ValveControl>();
builder.Services.AddSingleton<IReactorPressure, ReactorPressure>();
builder.Services.AddSingleton<IPressureSensor, PressureSensor>();
builder.Services.AddTransient<IReactorControlUnit, ReactorControlUnit>();
builder.Services.AddTransient<ISimulator, ReactorSimulator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors(x => x
   .AllowAnyMethod()
   .AllowAnyHeader()
   .SetIsOriginAllowed(origin => true) // allow any origin
   .AllowCredentials());

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Visualization}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "Api",
    pattern: "{controller=ReactorSimulator}/{action=Index}/{id?}");

app.Run();

