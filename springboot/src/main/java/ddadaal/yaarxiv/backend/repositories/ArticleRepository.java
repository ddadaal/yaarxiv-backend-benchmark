package ddadaal.yaarxiv.backend.repositories;

import ddadaal.yaarxiv.backend.entities.Article;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.JpaSpecificationExecutor;

public interface ArticleRepository extends JpaRepository<Article, Integer>, JpaSpecificationExecutor<Article> {

}