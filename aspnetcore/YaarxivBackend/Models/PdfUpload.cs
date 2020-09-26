using System;
using System.Collections.Generic;

namespace YaarxivBackend.Models
{
    public partial class PdfUpload
    {
        public PdfUpload()
        {
            ArticleRevision = new HashSet<ArticleRevision>();
        }

        public string Id { get; set; }
        public string UserId { get; set; }
        public string Link { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<ArticleRevision> ArticleRevision { get; set; }
    }
}
