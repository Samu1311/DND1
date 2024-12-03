using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;
    private ClaimsPrincipal _currentUser;

    public CustomAuthenticationStateProvider(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
        _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
    }

    /// <summary>
    /// Sets the authentication token and updates the authentication state.
    /// </summary>
    public async Task SetTokenAsync(string token)
    {
        await _localStorage.SetItemAsync("authToken", token);
        _currentUser = CreateClaimsPrincipalFromToken(token); // Update current user based on token
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    /// <summary>
    /// Logs out the user by clearing the token and resetting the authentication state.
    /// </summary>
    public async Task LogoutAsync()
    {
        await _localStorage.RemoveItemAsync("authToken");
        _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    /// <summary>
    /// Retrieves the current authentication state.
    /// </summary>
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _localStorage.GetItemAsync<string>("authToken");

        if (string.IsNullOrEmpty(token))
        {
            // If no token is found, return an anonymous user
            _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
            return new AuthenticationState(_currentUser);
        }

        _currentUser = CreateClaimsPrincipalFromToken(token);
        return new AuthenticationState(_currentUser);
    }

    /// <summary>
    /// Simulates a login for testing purposes with a given user type.
    /// </summary>
    public Task SimulateLogin(string userType)
    {
        var identity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, "TestUser"),
            new Claim("UserType", userType)
        }, "TestAuthType");

        _currentUser = new ClaimsPrincipal(identity);
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

        return Task.CompletedTask;
    }

    /// <summary>
    /// Helper method to create a ClaimsPrincipal from a JWT token.
    /// </summary>
    private ClaimsPrincipal CreateClaimsPrincipalFromToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        // Extract claims from the token
        var identity = new ClaimsIdentity(jwtToken.Claims, "jwt");
        return new ClaimsPrincipal(identity);
    }
}
