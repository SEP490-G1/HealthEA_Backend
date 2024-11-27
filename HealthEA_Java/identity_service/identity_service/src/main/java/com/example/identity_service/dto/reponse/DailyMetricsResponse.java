package com.example.identity_service.dto.reponse;

import jakarta.annotation.Nullable;
import lombok.*;
import lombok.experimental.FieldDefaults;

import java.util.UUID;

@Data
@NoArgsConstructor
@AllArgsConstructor
@Builder
@FieldDefaults(level = AccessLevel.PRIVATE)
public class DailyMetricsResponse {
    @NonNull
    String userId;

    @Nullable
    Float weight;

    @Nullable
    Float height;

    @Nullable
    Integer systolicBloodPressure;

    @Nullable
    Integer diastolicBloodPressure;

    @Nullable
    Integer heartRate;

    @Nullable
    Float bloodSugar;

    @Nullable
    Float bodyTemperature;

    @Nullable
    Float oxygenSaturation;
}
