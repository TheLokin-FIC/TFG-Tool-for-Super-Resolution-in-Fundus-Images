namespace Repository.Persistence.Models
{
    public class UserProfile
    {
        public long Id { get; set; }
        public string Role { get; set; }
        public string Username { get; set; }
        public string EncryptedPassword { get; set; }
    }
}