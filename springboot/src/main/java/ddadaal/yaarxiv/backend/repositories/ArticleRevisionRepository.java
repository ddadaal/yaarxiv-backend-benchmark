package ddadaal.yaarxiv.backend.repositories;

import ddadaal.yaarxiv.backend.entities.ArticleRevision;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.JpaSpecificationExecutor;

import java.util.List;
import java.util.Optional;

public interface ArticleRevisionRepository extends JpaRepository<ArticleRevision, Integer>, JpaSpecificationExecutor<ArticleRevision> {
    List<ArticleRevision> findByArticleId(int articleId);

    Optional<ArticleRevision> findByArticleIdAndRevisionNumber(int articleId, int revisionNumber);
}