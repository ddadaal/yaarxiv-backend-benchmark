using System;
using System.Collections.Generic;

namespace YaarxivBackend.Models
{
    public partial class Article
    {
        public Article()
        {
            ArticleRevision = new HashSet<ArticleRevision>();
        }

        public int Id { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public int LatestRevisionNumber { get; set; }
        public string OwnerId { get; set; }
        public bool OwnerSetPublicity { get; set; }
        public bool AdminSetPublicity { get; set; }

        public virtual User Owner { get; set; }
        public virtual ICollection<ArticleRevision> ArticleRevision { get; set; }
    }
}
