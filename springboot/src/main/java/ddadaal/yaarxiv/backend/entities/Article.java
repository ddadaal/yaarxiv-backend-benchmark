package ddadaal.yaarxiv.backend.entities;

import lombok.Data;

import javax.persistence.*;
import java.io.Serializable;
import java.time.LocalDateTime;

@Entity
@Table(name = "article")
@Data
public class Article implements Serializable {

    private static final long serialVersionUID = 1L;

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id", nullable = false)
    private Integer id;

    @Column(name = "createTime", nullable = false)
    private LocalDateTime createTime;

    @Column(name = "lastUpdateTime", nullable = false)
    private LocalDateTime lastUpdateTime;

    @Column(name = "latestRevisionNumber", nullable = false)
    private Integer latestRevisionNumber;

    @Column(name = "ownerId", nullable = false)
    private String ownerId;

    @Column(name = "ownerSetPublicity", nullable = false)
    private boolean ownerSetPublicity;

    @Column(name = "adminSetPublicity", nullable = false)
    private boolean adminSetPublicity;

}
