using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace Portfolio.Wasm.Services;

public class SupabaseAuthStateProvider : AuthenticationStateProvider
{
    private readonly Supabase.Client? _supabase;
    private readonly IJSRuntime? _js;
    private ClaimsPrincipal _currentUser = new(new ClaimsIdentity());
    private bool _hasCheckedLocalAuth = false;

    public SupabaseAuthStateProvider() : this(null, null) { }

    public SupabaseAuthStateProvider(Supabase.Client? supabase = null, IJSRuntime? js = null)
    {
        _supabase = supabase;
        _js = js;
        if (_supabase?.Auth != null)
        {
            _supabase.Auth.AddStateChangedListener((sender, state) =>
            {
                NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            });
        }
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
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
            return new AuthenticationState(_currentUser);
        }

        if (_currentUser.Identity?.IsAuthenticated == true)
        {
            return new AuthenticationState(_currentUser);
        }

        if (!_hasCheckedLocalAuth && _js != null)
        {
            _hasCheckedLocalAuth = true;
            try
            {
                var storedAuth = await _js.InvokeAsync<string?>("portfolioStorage.get", "portfolio_auth_user");
                if (!string.IsNullOrWhiteSpace(storedAuth))
                {
                    var identity = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.Email, storedAuth),
                        new Claim(ClaimTypes.Name, storedAuth.Contains("@") ? storedAuth.Split('@')[0] : storedAuth)
                    }, "StoredAuth");
                    _currentUser = new ClaimsPrincipal(identity);
                    return new AuthenticationState(_currentUser);
                }
            }
            catch { }
        }

        _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
        return new AuthenticationState(_currentUser);
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
                    if (_js != null)
                    {
                        try { await _js.InvokeVoidAsync("portfolioStorage.set", "portfolio_auth_user", response.User.Email ?? email); } catch { }
                    }
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
            if (_js != null)
            {
                try { await _js.InvokeVoidAsync("portfolioStorage.set", "portfolio_auth_user", "admin@erick.dev"); } catch { }
            }
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

        if (_js != null)
        {
            try { await _js.InvokeVoidAsync("portfolioStorage.remove", "portfolio_auth_user"); } catch { }
        }

        _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
    }
}
