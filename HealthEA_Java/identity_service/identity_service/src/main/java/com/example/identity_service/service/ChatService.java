package com.example.identity_service.service;

import com.example.identity_service.dto.reponse.DailyMetricsResponse;
import okhttp3.*;
import org.json.JSONArray;
import org.json.JSONObject;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;

import java.io.IOException;
import java.util.concurrent.TimeUnit;

@Service
public class ChatService {
    @Value("${openai.api-key}")
    private String apiKey;

    @Value("${openai.url}")
    private String apiUrl;

    public String getDailyMetricsAdvice(DailyMetricsResponse dailyMetricsResponse, String question, String language) throws Exception {
        // Xây dựng prompt dựa trên ngôn ngữ
        String prompt;
        if ("vi".equalsIgnoreCase(language)) {
            prompt = "Người dùng có tình trạng sức khỏe sau: " +
                    "Cân nặng: " + dailyMetricsResponse.getWeight() +
                    "Chiều cao: " + dailyMetricsResponse.getHeight() +
                    "Huyết áp: " + dailyMetricsResponse.getSystolicBloodPressure() + "/" + dailyMetricsResponse.getDiastolicBloodPressure() +
                    "Nhịp tim: " + dailyMetricsResponse.getHeartRate() +
                    "Đường huyết: " + dailyMetricsResponse.getBloodSugar() +
                    "Nhiệt độ: " + dailyMetricsResponse.getBodyTemperature() +
                    "Nồng độ oxy trong máu: " + dailyMetricsResponse.getWeight() +
                    ". Câu hỏi: " + question +
                    ". Hãy trả lời bằng tiếng Việt và đưa ra các gợi ý chi tiết.";
        } else {
            prompt = "The user has the following health condition: " + dailyMetricsResponse +
                    ". Question: " + question +
                    ". Please provide a detailed response in English.";
        }

        // Tạo HTTP client với thời gian chờ cao hơn
        OkHttpClient client = new OkHttpClient.Builder()
                .connectTimeout(30, TimeUnit.SECONDS)
                .writeTimeout(30, TimeUnit.SECONDS)
                .readTimeout(30, TimeUnit.SECONDS)
                .build();

        // Xây dựng JSON body cho request
        JSONObject jsonBody = new JSONObject();
        jsonBody.put("model", "gpt-3.5-turbo"); // Hoặc "gpt-3.5-turbo" nếu bạn không có quyền truy cập GPT-4
        jsonBody.put("messages", new JSONArray()
                .put(new JSONObject()
                        .put("role", "system")
                        .put("content", "You are a health assistant providing advice based on medical information."))
                .put(new JSONObject()
                        .put("role", "user")
                        .put("content", prompt))
        );

        // Xây dựng request body
        RequestBody body = RequestBody.create(
                jsonBody.toString(),
                MediaType.get("application/json; charset=utf-8")
        );

        // Xây dựng HTTP request
        Request request = new Request.Builder()
                .url(apiUrl)
                .header("Authorization", "Bearer " + apiKey)
                .post(body)
                .build();

        // Gửi request và xử lý response
        try (Response response = client.newCall(request).execute()) {
            if (!response.isSuccessful()) {
                throw new IOException("Unexpected code " + response);
            }

            // Phân tích phản hồi JSON
            String responseData = response.body().string();
            JSONObject jsonResponse = new JSONObject(responseData);
            JSONArray choices = jsonResponse.getJSONArray("choices");
            return choices.getJSONObject(0).getJSONObject("message").getString("content");
        }
    }
}
