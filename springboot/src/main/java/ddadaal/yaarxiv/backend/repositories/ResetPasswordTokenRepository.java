package ddadaal.yaarxiv.backend.repositories;

import ddadaal.yaarxiv.backend.entities.ResetPasswordToken;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.JpaSpecificationExecutor;

public interface ResetPasswordTokenRepository extends JpaRepository<ResetPasswordToken, String>, JpaSpecificationExecutor<ResetPasswordToken> {

}