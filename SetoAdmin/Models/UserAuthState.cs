namespace SetoAdmin.Models
{
    public class UserAuthState
    {
        public bool IsAuthenticated { get; set; }
        public string Token { get; set; }
        public string UserEmail { get; set; }
    }
}
