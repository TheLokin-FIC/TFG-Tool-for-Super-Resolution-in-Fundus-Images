using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web.Components.Session
{
    public class SessionProvider : AuthenticationStateProvider
    {
        private const string USER_SESSION_KEY = "user_session";

        //private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ISessionStorageService sessionStorage;

        public SessionProvider(ISessionStorageService sessionStorage)
        {
            //this.httpContextAccessor = httpContextAccessor;
            this.sessionStorage = sessionStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            Session session = await sessionStorage.GetItemAsync<Session>(USER_SESSION_KEY);

            if (session != null)
            {
                return await GenerateAuthenticationState(session);
            }
            else
            {
                return await GenerateEmptyAuthenticationState();
            }
        }

        public async Task<bool> IsAuthenticated()
        {
            return await sessionStorage.GetItemAsync<Session>(USER_SESSION_KEY) != null;
        }

        public async Task<Session> Session()
        {
            return await sessionStorage.GetItemAsync<Session>(USER_SESSION_KEY);
        }

        public async Task LoginAsync(long userId, string role)
        {
            Session session = new()
            {
                Id = userId,
                Role = role
            };

            await sessionStorage.SetItemAsync(USER_SESSION_KEY, session);
            NotifyAuthenticationStateChanged(GenerateAuthenticationState(session));
        }

        public async Task LogoutAsync()
        {
            await sessionStorage.RemoveItemAsync(USER_SESSION_KEY);

            NotifyAuthenticationStateChanged(GenerateEmptyAuthenticationState());
        }

        private static Task<AuthenticationState> GenerateAuthenticationState(Session session)
        {
            ClaimsIdentity identity = new(new[]
            {
                new Claim(ClaimTypes.Name, session.Id.ToString()),
                new Claim(ClaimTypes.Role, session.Role)
            }, "apiauth_type");

            return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
        }

        private static Task<AuthenticationState> GenerateEmptyAuthenticationState()
        {
            return Task.FromResult(new AuthenticationState(new ClaimsPrincipal()));
        }
    }
}