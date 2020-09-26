package ddadaal.yaarxiv.backend.repositories;

import ddadaal.yaarxiv.backend.entities.PdfUpload;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.JpaSpecificationExecutor;

public interface PdfUploadRepository extends JpaRepository<PdfUpload, String>, JpaSpecificationExecutor<PdfUpload> {

}