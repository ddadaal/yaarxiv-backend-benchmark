package ddadaal.yaarxiv.backend.repositories;

import ddadaal.yaarxiv.backend.entities.ArticleRevision;
import ddadaal.yaarxiv.backend.entities.ArticleRevisionInfo;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.JpaSpecificationExecutor;

import java.util.List;
import java.util.Optional;

public interface ArticleRevisionRepository extends JpaRepository<ArticleRevision, Integer>, JpaSpecificationExecutor<ArticleRevision> {
    List<ArticleRevisionInfo> findByArticleId(int articleId);

    Optional<ArticleRevision> findByArticleIdAndRevisionNumber(int articleId, int revisionNumber);
}