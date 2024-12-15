package com.example.identity_service.controller;

import com.example.identity_service.dto.request.ApiResponse;
import com.example.identity_service.dto.reponse.DailyMetricsResponse;
import com.example.identity_service.service.DailyMetricsService;
import lombok.AccessLevel;
import lombok.RequiredArgsConstructor;
import lombok.experimental.FieldDefaults;
import lombok.extern.slf4j.Slf4j;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.UUID;

@RestController
@RequestMapping("/dailymetrics")
@RequiredArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE, makeFinal = true)
@Slf4j

public class DailyMetricsController {

    @Autowired
    DailyMetricsService dailyMetricsService;

    @PostMapping("/getLatest")
    public ApiResponse<DailyMetricsResponse> sendVerificationLink() {
        return ApiResponse.<DailyMetricsResponse>builder()
                .result(dailyMetricsService.getLatestDailyMetrics())
                .build();
    }
}
