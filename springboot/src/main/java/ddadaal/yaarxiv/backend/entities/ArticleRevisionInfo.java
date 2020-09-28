package ddadaal.yaarxiv.backend.entities;

import java.time.LocalDateTime;

public interface ArticleRevisionInfo {
    LocalDateTime getTime();
    int getRevisionNumber();
}
