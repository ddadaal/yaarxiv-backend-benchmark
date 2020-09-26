package ddadaal.yaarxiv.backend.controllers;

import com.fasterxml.jackson.annotation.JsonProperty;
import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.JsonSerializer;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.fasterxml.jackson.databind.type.TypeFactory;
import ddadaal.yaarxiv.backend.repositories.ArticleRepository;
import ddadaal.yaarxiv.backend.repositories.ArticleRevisionRepository;
import ddadaal.yaarxiv.backend.repositories.PdfUploadRepository;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.SneakyThrows;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;

import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;

@Data
class Author {
    public String name;
    public Optional<String> affiliation;
}

@Data
@AllArgsConstructor
class ArticleRevision {
    public int number;
    public LocalDateTime time;
}

@Data
@AllArgsConstructor
class ArticleInfo {
    public String title;
    public List<String> authors;
    @JsonProperty("abstract")
    public String articleAbstract;
    public List<String> keywords;
    public String category;
    public String pdfLink;
}

@Data
@AllArgsConstructor
class GetArticleResult {
    public String id;
    public int revisionNumber;
    public ArticleInfo currentRevision;
    public List<ArticleRevision> revisions;
    public String ownerId;
    public LocalDateTime createTime;
}

@AllArgsConstructor
@Data
class Response {
    public GetArticleResult article;
}

@AllArgsConstructor
@ResponseStatus(value = HttpStatus.NOT_FOUND)
class FailedException extends RuntimeException {
    public String notFound;
}

@RestController
public class ArticleController {

    private final ArticleRepository articleRepository;
    private final ArticleRevisionRepository articleRevisionRepository;
    private final PdfUploadRepository pdfUploadRepository;


    public ArticleController(ArticleRepository articleRepository, ArticleRevisionRepository articleRevisionRepository, PdfUploadRepository pdfUploadRepository) {
        this.articleRepository = articleRepository;
        this.articleRevisionRepository = articleRevisionRepository;
        this.pdfUploadRepository = pdfUploadRepository;
    }


    @SneakyThrows
    @GetMapping("/articles/{id}")
    public Response getArticle(@PathVariable("id") int articleId, @RequestParam Optional<Integer> revisionNumber) {

        var article = articleRepository.findById(articleId).orElseThrow(() -> new FailedException("article"));

        if (!article.isAdminSetPublicity() || !article.isOwnerSetPublicity()) {
            throw new FailedException("article");
        }

        var articleRevisionInfo = articleRevisionRepository.findByArticleId(articleId);

        var targetRevisionNumber = revisionNumber.orElse(article.getLatestRevisionNumber());

        var targetRevision = articleRevisionRepository
            .findByArticleIdAndRevisionNumber(articleId, targetRevisionNumber)
            .orElseThrow(() -> new FailedException("revision"));

        var pdf = pdfUploadRepository
            .findById(targetRevision.getPdfId())
            .orElseThrow(() -> new FailedException("pdf"));

        return new Response(
            new GetArticleResult(
                articleId + "",
                targetRevisionNumber,
                new ArticleInfo(
                    targetRevision.getAbstractContent(),
                    new ObjectMapper().readValue(
                        targetRevision.getAuthors(),
                        new TypeReference<ArrayList<Author>>() {}
                    ).stream()
                        .map(Author::getName)
                        .collect(Collectors.toList()),
                    targetRevision.getAbstractContent(),
                    Arrays.asList(targetRevision.getKeywords().split(",")),
                    pdf.getLink(),
                    targetRevision.getTitle()
                ),
                articleRevisionInfo.stream().map(x -> new ArticleRevision(
                    x.getRevisionNumber(),
                    x.getTime()
                )).collect(Collectors.toList()),
                article.getOwnerId(),
                article.getCreateTime()
            )
        );


    }
}
