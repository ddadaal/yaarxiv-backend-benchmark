using System;
using System.Collections.Generic;

namespace YaarxivBackend.Models
{
    public partial class User
    {
        public User()
        {
            Article = new HashSet<Article>();
            PdfUpload = new HashSet<PdfUpload>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public virtual ICollection<Article> Article { get; set; }
        public virtual ICollection<PdfUpload> PdfUpload { get; set; }
    }
}
