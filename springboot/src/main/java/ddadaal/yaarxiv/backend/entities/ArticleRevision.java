package ddadaal.yaarxiv.backend.entities;

import lombok.Data;

import javax.persistence.*;
import java.io.Serializable;
import java.time.LocalDateTime;

@Entity
@Data
@Table(name = "article_revision")
public class ArticleRevision implements Serializable {

    private static final long serialVersionUID = 1L;

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id", nullable = false)
    private Integer id;

    @Column(name = "revisionNumber", nullable = false)
    private Integer revisionNumber;

    @Column(name = "time", nullable = false)
    private LocalDateTime time;

    @Column(name = "title", nullable = false)
    private String title;

    @Column(name = "authors", nullable = false)
    private String authors;

    @Column(name = "keywords", nullable = false)
    private String keywords;

    @Column(name = "abstract", nullable = false)
    private String abstractContent;

    @Column(name = "category", nullable = false)
    private String category;

    @Column(name = "articleId", nullable = false)
    private Integer articleId;

    @Column(name = "pdfId")
    private String pdfId;

}
