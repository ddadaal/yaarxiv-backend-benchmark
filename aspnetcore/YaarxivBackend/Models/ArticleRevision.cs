using System;
using System.Collections.Generic;

namespace YaarxivBackend.Models
{
    public partial class ArticleRevision
    {
        public int Id { get; set; }
        public int RevisionNumber { get; set; }
        public DateTime Time { get; set; }
        public string Title { get; set; }
        public string Authors { get; set; }
        public string Keywords { get; set; }
        public string Abstract { get; set; }
        public string Category { get; set; }
        public int ArticleId { get; set; }
        public string PdfId { get; set; }

        public virtual Article Article { get; set; }
        public virtual PdfUpload Pdf { get; set; }
    }
}
