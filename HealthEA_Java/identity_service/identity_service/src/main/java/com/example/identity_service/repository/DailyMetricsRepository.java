package com.example.identity_service.repository;

import com.example.identity_service.entity.DailyMetrics;
import com.example.identity_service.entity.User;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;

import java.util.UUID;

@Repository
public interface DailyMetricsRepository extends JpaRepository<DailyMetrics, String> {
    @Query("SELECT MAX(d.weight) FROM DailyMetrics d WHERE d.userId = :userId")
    Float findMaxWeightByUserId(String userId);

    @Query("SELECT MAX(d.height) FROM DailyMetrics d WHERE d.userId = :userId")
    Float findMaxHeightByUserId(String userId);

    @Query("SELECT MAX(d.systolicBloodPressure) FROM DailyMetrics d WHERE d.userId = :userId")
    Integer  findMaxSystolicBloodPressureByUserId(String userId);

    @Query("SELECT MAX(d.diastolicBloodPressure) FROM DailyMetrics d WHERE d.userId = :userId")
    Integer findMaxDiastolicBloodPressureByUserId(String userId);

    @Query("SELECT MAX(d.heartRate) FROM DailyMetrics d WHERE d.userId = :userId")
    Integer findMaxHeartRateByUserId(String userId);

    @Query("SELECT MAX(d.bloodSugar) FROM DailyMetrics d WHERE d.userId = :userId")
    Float findMaxBloodSugarByUserId(String userId);

    @Query("SELECT MAX(d.bodyTemperature) FROM DailyMetrics d WHERE d.userId = :userId")
    Float findMaxBodyTemperatureByUserId(String userId);

    @Query("SELECT MAX(d.oxygenSaturation) FROM DailyMetrics d WHERE d.userId = :userId")
    Float findMaxOxygenSaturationByUserId(String userId);
}
