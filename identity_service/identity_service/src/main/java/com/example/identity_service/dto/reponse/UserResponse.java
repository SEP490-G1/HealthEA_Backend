package com.example.identity_service.dto.reponse;

import com.example.identity_service.entity.Role;
import com.example.identity_service.enums.Status;
import jakarta.validation.constraints.Size;
import lombok.*;
import lombok.experimental.FieldDefaults;

import java.time.LocalDate;
import java.util.Set;

@Data
@NoArgsConstructor
@AllArgsConstructor
@Builder
@FieldDefaults(level = AccessLevel.PRIVATE)
public class UserResponse {
    String id;
    String username;
    String email;
    String firstName;
    String lastName;
    String phone;
    LocalDate dob;
    Boolean gender;
    Role role;
    Status status;
}
