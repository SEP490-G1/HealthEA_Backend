package com.example.identity_service.dto.reponse;

import com.example.identity_service.enums.Role;
import com.example.identity_service.enums.Status;
import lombok.*;
import lombok.experimental.FieldDefaults;

import java.time.LocalDate;

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
    private String avatar;

}
