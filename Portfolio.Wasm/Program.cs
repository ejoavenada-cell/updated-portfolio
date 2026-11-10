using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Portfolio.Wasm;
using Portfolio.Wasm.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Configure Supabase Client if valid credentials are provided
var supabaseUrl = builder.Configuration["Supabase:Url"];
var supabaseKey = builder.Configuration["Supabase:AnonKey"];

Supabase.Client? supabaseClient = null;
if (!string.IsNullOrWhiteSpace(supabaseUrl) && 
    !string.IsNullOrWhiteSpace(supabaseKey) && 
    !supabaseUrl.Contains("YOUR_PROJECT_ID") &&
    Uri.TryCreate(supabaseUrl, UriKind.Absolute, out _))
{
    try
    {
        var options = new Supabase.SupabaseOptions
        {
            AutoRefreshToken = true,
            AutoConnectRealtime = true
        };
        supabaseClient = new Supabase.Client(supabaseUrl, supabaseKey, options);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Supabase client initialization notice: {ex.Message}");
    }
}

if (supabaseClient != null)
{
    builder.Services.AddSingleton(supabaseClient);
}

// Authentication & Core Services
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped(sp => new SupabaseAuthStateProvider(sp.GetService<Supabase.Client>()));
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<SupabaseAuthStateProvider>());
builder.Services.AddScoped(sp => new PortfolioDataService(sp.GetService<Supabase.Client>()));
builder.Services.AddScoped(sp => new StorageService(sp.GetService<Supabase.Client>()));

var host = builder.Build();

// Async initialization of Supabase client if present
if (supabaseClient != null)
{
    try
    {
        await supabaseClient.InitializeAsync();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Supabase client async connection notice: {ex.Message}");
    }
}

await host.RunAsync();
