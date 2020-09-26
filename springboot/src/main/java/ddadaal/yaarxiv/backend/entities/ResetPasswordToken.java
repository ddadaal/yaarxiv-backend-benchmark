package ddadaal.yaarxiv.backend.entities;

import lombok.Data;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.Table;
import java.io.Serializable;
import java.time.LocalDateTime;

@Table(name = "reset_password_token")
@Entity
@Data
public class ResetPasswordToken implements Serializable {

    private static final long serialVersionUID = 1L;

    @Id
    @Column(name = "id", nullable = false)
    private String id;

    @Column(name = "userEmail", nullable = false)
    private String userEmail;

    @Column(name = "time", nullable = false)
    private LocalDateTime time;

}
