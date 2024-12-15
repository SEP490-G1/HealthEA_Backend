package com.example.identity_service.entity;

import jakarta.persistence.*;
import lombok.*;
import lombok.experimental.FieldDefaults;

import java.util.UUID;

@Entity
@Data
@Builder
@NoArgsConstructor
@AllArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE)
@Table(name = "Doctors")
public class Doctor {

    @Id
//    @GeneratedValue(strategy = GenerationType.AUTO)
    @Column(name = "Id", columnDefinition = "uniqueidentifier")
    private String id;

    @Column(name = "UserId", nullable = false)
    private String userId;

    @Lob
    @Column(name = "Description" , columnDefinition = "nvarchar(MAX)")
    private String description;

    @Lob
    @Column(name = "ClinicAddress" , columnDefinition = "nvarchar(MAX)")
    private String clinicAddress;

    @Lob
    @Column(name = "ClinicCity" , columnDefinition = "nvarchar(MAX)")
    private String clinicCity;

    @Column(name = "DisplayName", nullable = false)
    private String displayName;

    @Lob
    @Column(name = "HistoryOfWork")
    private String historyOfWork;

    @Column(name = "NumberOfAppointments")
    private Integer numberOfAppointments;

    @Column(name = "NumberOfVideoCalls")
    private Integer numberOfVideoCalls;

    @Lob
    @Column(name = "Specialization", columnDefinition = "nvarchar(MAX)")
    private String specialization;

    @PrePersist
    public void generateId() {
        if (this.id == null) {
            this.id = UUID.randomUUID().toString();  // Tạo UUID mới nếu chưa có
        }
    }
}
