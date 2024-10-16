package com.example.identity_service.entity;

import com.example.identity_service.enums.Role;
import com.example.identity_service.enums.Status;
import jakarta.persistence.*;
import lombok.*;
import lombok.experimental.FieldDefaults;
import org.hibernate.annotations.SQLRestriction;

import java.time.LocalDate;

@Entity
@Data
@Builder
@NoArgsConstructor
@AllArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE)
@Table(name = "[user]")
public class User {
    @Id
    @GeneratedValue(strategy = GenerationType.UUID)
    @Column(name = "user_id")
    String id;
    @Column(name = "username", nullable = false, unique = true)
    String username;
    @Column(name = "email", nullable = false, unique = true)
    String email;
    @Column(name = "password", nullable = false)
    String password;
    @Column(name = "first_name")
    String firstName;
    @Column(name = "last_name")
    String lastName;
    @Column(name = "phone")
    String phone;
    @Column(name = "dob")
    LocalDate dob;
    @Column(name = "gender")
    Boolean gender;
    @Column(name = "role")
    @Enumerated(EnumType.STRING)
    Role role;
    @Column(name = "status")
    @Enumerated(EnumType.STRING)
    Status status;
}
