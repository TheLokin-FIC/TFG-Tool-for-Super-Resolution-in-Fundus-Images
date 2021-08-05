using System;
using System.Collections.Generic;

namespace Repository.Persistence.Models
{
    public class Dataset
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Title { get; set; }
        public bool Public { get; set; }
        public DateTime LastModification { get; set; }

        public UserProfile UserProfile { get; set; }
        public ICollection<Image> Images { get; set; }
    }
}