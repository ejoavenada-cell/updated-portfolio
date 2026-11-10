using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace Portfolio.Wasm.Services;

public class SupabaseAuthStateProvider : AuthenticationStateProvider
{
    private readonly Supabase.Client? _supabase;
    private ClaimsPrincipal _currentUser = new(new ClaimsIdentity());

    public SupabaseAuthStateProvider(Supabase.Client? supabase = null)
    {
        _supabase = supabase;
        if (_supabase?.Auth != null)
        {
            _supabase.Auth.AddStateChangedListener((sender, state) =>
            {
                NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            });
        }
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var session = _supabase?.Auth?.CurrentSession;
        if (session?.User != null)
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, session.User.Id ?? ""),
                new Claim(ClaimTypes.Email, session.User.Email ?? ""),
                new Claim(ClaimTypes.Name, session.User.Email ?? "Admin")
            }, "SupabaseAuth");
            _currentUser = new ClaimsPrincipal(identity);
        }
        else if (_currentUser.Identity?.IsAuthenticated == true)
        {
            // Keep active session
        }
        else
        {
            _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
        }

        return Task.FromResult(new AuthenticationState(_currentUser));
    }

    public async Task<bool> SignInAsync(string email, string password)
    {
        if (_supabase?.Auth != null)
        {
            try
            {
                var response = await _supabase.Auth.SignIn(email, password);
                if (response?.User != null)
                {
                    NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Supabase sign in error: {ex.Message}");
            }
        }

        // Demo / dev fallback mode
        if ((email.Equals("admin@erick.dev", StringComparison.OrdinalIgnoreCase) || email.Equals("admin", StringComparison.OrdinalIgnoreCase)) && 
            password == "admin123")
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Email, "admin@erick.dev"),
                new Claim(ClaimTypes.Name, "Erick (Admin)")
            }, "DemoAuth");
            _currentUser = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
            return true;
        }

        return false;
    }

    public async Task SignOutAsync()
    {
        if (_supabase?.Auth != null)
        {
            try
            {
                await _supabase.Auth.SignOut();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Supabase sign out error: {ex.Message}");
            }
        }

        _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
    }
}
