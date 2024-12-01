package com.example.identity_service.dto.request;

import com.example.identity_service.enums.Role;
import com.example.identity_service.enums.Status;
import com.example.identity_service.validator.DobContraints;
import jakarta.validation.constraints.Size;
import lombok.*;
import lombok.experimental.FieldDefaults;

import java.time.LocalDate;

@Data
@NoArgsConstructor
@AllArgsConstructor
@Builder
@FieldDefaults(level = AccessLevel.PRIVATE)
public class UserCreationRequest {
    @Size(min = 4, message = "USERNAME_INVALID")
    String username;
    @Size(min = 8, message = "PASSWORD_INVALID")
    String password;
    String email;
    String firstName;
    String lastName;
    String phone;
    @DobContraints(min = 18, message = "DOB_INVALID")
    LocalDate dob;
    Boolean gender;
    Role role;
    Status status;
    String avatar;
}