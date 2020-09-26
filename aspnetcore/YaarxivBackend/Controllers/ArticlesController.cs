using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using YaarxivBackend.Models;

namespace YaarxivBackend.Controllers
{
    public class Author
    {
        public string Name { get; set; }
        public string? Affiliation { get; set; }
    }

    public class ArticleRevision
    {
        public int Number { get; set; }
        public DateTime Time { get; set; }
    }
    public class ArticleInfo
    {
        public string Title { get; set; }
        public IEnumerable<string> Authors { get; set; }
        public string Abstract { get; set; }
        public IEnumerable<string> Keywords { get; set; }
        public string Category { get; set; }
        public string PdfLink { get; set; }
    }

    public class GetArticleResult
    {
        public string Id { get; set; }
        public int RevisionNumber { get; set; }
        public ArticleInfo CurrentRevision { get; set; }
        public IEnumerable<ArticleRevision> Revisions { get; set; }
        public string OwnerId { get; set; }
        public DateTime CreateTime { get; set; }
    }

    [ApiController]
    [Route("articles")]
    public class ArticlesController : ControllerBase
    {

        private readonly ILogger<ArticlesController> _logger;
        private readonly ArticleContext articleContext;

        public ArticlesController(ILogger<ArticlesController> logger, ArticleContext articleContext)
        {
            _logger = logger;
            this.articleContext = articleContext;
        }

        public class Query
        {
            public int? RevisionNumber { get; set; }
        }

        public class Response
        {
            public GetArticleResult Article { get; set; }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> GetArticle([FromRoute] int id, [FromQuery] Query query)
        {
            var article = await articleContext.Article.FindAsync(id);
            if (article == null)
            {
                return NotFound(new { NotFound = "article" });
            }

            if (!article.AdminSetPublicity || !article.OwnerSetPublicity)
            {
                return NotFound(new { NotFound = "article" });
            }

            var articleRevisionInfo = article.ArticleRevision.OrderByDescending(x => x.Time);

            var targetRevisionNumber = query.RevisionNumber ?? article.LatestRevisionNumber;

            var targetRevision = articleContext.ArticleRevision
                .Where(x => x.ArticleId == id && x.RevisionNumber == targetRevisionNumber)
                .FirstOrDefault();

            if (targetRevision == null)
            {
                return NotFound(new { NotFound = "revision" });
            }

            return new Response
            {
                Article = new GetArticleResult
                {
                    Id = id.ToString(),
                    RevisionNumber = targetRevisionNumber,
                    CurrentRevision = new ArticleInfo
                    {
                        Abstract = targetRevision.Abstract,
                        Authors = JsonSerializer.Deserialize<List<Author>>(targetRevision.Authors).Select(x => x.Name),
                        Category = targetRevision.Category,
                        Keywords = targetRevision.Keywords.Split(","),
                        PdfLink = targetRevision.Pdf.Link,
                        Title = targetRevision.Title,
                    },
                    Revisions = articleRevisionInfo.Select(x => new ArticleRevision
                    {
                        Time = x.Time,
                        Number = x.RevisionNumber,
                    }),
                    OwnerId = article.OwnerId,
                    CreateTime = article.CreateTime,
                }
            };

        }
    }
}
