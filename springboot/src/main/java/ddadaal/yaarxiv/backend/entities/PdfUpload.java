package ddadaal.yaarxiv.backend.entities;

import lombok.Data;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.Table;
import java.io.Serializable;

@Entity
@Table(name = "pdf_upload")
@Data
public class PdfUpload implements Serializable {

    private static final long serialVersionUID = 1L;

    @Id
    @Column(name = "id", nullable = false)
    private String id;

    @Column(name = "userId", nullable = false)
    private String userId;

    @Column(name = "link", nullable = false)
    private String link;

}
