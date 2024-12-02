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
    @GeneratedValue(strategy = GenerationType.AUTO)
    @Column(name = "Id", columnDefinition = "uniqueidentifier")
    private String id;

    @Column(name = "UserId", nullable = false)
    private String userId;

    @Lob
    @Column(name = "Description")
    private String description;

    @Lob
    @Column(name = "ClinicAddress")
    private String clinicAddress;

    @Lob
    @Column(name = "ClinicCity")
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
    @Column(name = "Specialization")
    private String specialization;
}
