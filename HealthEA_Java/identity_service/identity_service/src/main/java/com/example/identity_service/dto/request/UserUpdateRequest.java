package com.example.identity_service.dto.request;

import com.example.identity_service.enums.Role;
import com.example.identity_service.enums.Status;
import com.example.identity_service.validator.DobContraints;
import lombok.*;
import lombok.experimental.FieldDefaults;

import java.time.LocalDate;

@Data
@NoArgsConstructor
@AllArgsConstructor
@Builder
@FieldDefaults(level = AccessLevel.PRIVATE)
public class UserUpdateRequest {

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
