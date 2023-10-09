namespace SweatFlex.Maui.Services
{
    public class AuthService
    {
        public async Task<bool> LoginAsync(string email, string password)
        {
            
            return true;
        }

        public async Task<bool> RegisterAsync(string email, string password)
        {
            await Task.Delay(1000);
            return true;
        }
    }
}
