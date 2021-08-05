using System.Collections.Generic;

namespace Repository.Persistence.Models
{
    public class UserProfile
    {
        public long Id { get; set; }
        public string Role { get; set; }
        public string Username { get; set; }
        public string EncryptedPassword { get; set; }

        public ICollection<Dataset> Datasets { get; set; }
    }
}