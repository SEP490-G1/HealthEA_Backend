package com.example.identity_service.entity;

import jakarta.persistence.*;
import lombok.*;
import lombok.experimental.FieldDefaults;

import java.time.LocalDate;
import java.util.UUID;


@Data
@Builder
@NoArgsConstructor
@AllArgsConstructor
@Entity
@FieldDefaults(level = AccessLevel.PRIVATE)
@Table(name = "DailyMetrics")
public class DailyMetrics {

    @Id
    @Column(name = "id")
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    String id;

    @Column(name = "UserId")
    String userId;

    @Column(name = "Weight")
    Float weight;

    @Column(name = "Height")
    Double height;

    @Column(name = "SystolicBloodPressure")
    Integer systolicBloodPressure;

    @Column(name = "DiastolicBloodPressure")
    Integer diastolicBloodPressure;

    @Column(name = "HeartRate")
    Integer heartRate;

    @Column(name = "BloodSugar")
    Double bloodSugar;

    @Column(name = "BodyTemperature")
    Double bodyTemperature;

    @Column(name = "OxygenSaturation")
    Double oxygenSaturation;

    @Column(name = "Date")
    LocalDate Date;
}

