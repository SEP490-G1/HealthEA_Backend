package com.example.identity_service.service;

import com.example.identity_service.dto.reponse.DailyMetricsResponse;
import com.example.identity_service.repository.DailyMetricsRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.UUID;

@Service
public class DailyMetricsService {
    private final DailyMetricsRepository dailyMetricsRepository;

    @Autowired
    public DailyMetricsService(DailyMetricsRepository dailyMetricsRepository) {
        this.dailyMetricsRepository = dailyMetricsRepository;
    }

    public DailyMetricsResponse getLatestDailyMetrics(String userId) {
        DailyMetricsResponse dailyMetricsResponse = new DailyMetricsResponse(userId, dailyMetricsRepository.findMaxWeightByUserId(userId),
                dailyMetricsRepository.findMaxHeightByUserId(userId),
                dailyMetricsRepository.findMaxSystolicBloodPressureByUserId(userId),
                dailyMetricsRepository.findMaxDiastolicBloodPressureByUserId(userId),
                dailyMetricsRepository.findMaxHeartRateByUserId(userId),
                dailyMetricsRepository.findMaxBloodSugarByUserId(userId),
                dailyMetricsRepository.findMaxBodyTemperatureByUserId(userId),
                dailyMetricsRepository.findMaxOxygenSaturationByUserId(userId)
        );

        return dailyMetricsResponse;

    }

}
