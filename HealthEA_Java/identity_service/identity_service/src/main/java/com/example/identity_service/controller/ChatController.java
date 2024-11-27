package com.example.identity_service.controller;

import com.example.identity_service.dto.reponse.DailyMetricsResponse;
import com.example.identity_service.dto.request.ApiResponse;
import com.example.identity_service.service.ChatService;
import com.example.identity_service.service.DailyMetricsService;
import lombok.AccessLevel;
import lombok.RequiredArgsConstructor;
import lombok.experimental.FieldDefaults;
import lombok.extern.slf4j.Slf4j;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.messaging.handler.annotation.MessageMapping;
import org.springframework.messaging.simp.SimpMessagingTemplate;
import org.springframework.web.bind.annotation.*;

@CrossOrigin(origins = "http://localhost:5173/")
@RestController
@RequestMapping("/dailymetrics")
@RequiredArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE, makeFinal = true)
@Slf4j

public class ChatController {

    private final SimpMessagingTemplate messagingTemplate;
    private final ChatService chatService;
    private final DailyMetricsService dailyMetricsService;

    public ChatController(SimpMessagingTemplate messagingTemplate, ChatService chatService, DailyMetricsService dailyMetricsService) {
        this.messagingTemplate = messagingTemplate;
        this.chatService = chatService;
        this.dailyMetricsService = dailyMetricsService;
    }

    @MessageMapping("/sendMessage")
    public void handleMessage(String userId) throws Exception {
        // Truy xuất thông tin metrics gần nhất
        var latestMetrics = dailyMetricsService.getLatestDailyMetrics(userId);

        // Lấy lời khuyên từ AI dựa trên dữ liệu metrics
        String advice = chatService.getDailyMetricsAdvice(latestMetrics, "Tinh trang suc khoe dang nhu vao", "vi");

        // Gửi lời khuyên tới client qua WebSocket
        messagingTemplate.convertAndSend("/topic/public", advice);
    }

    @PostMapping("/getAiResponse/{userId}")
    public ApiResponse<String> sendVerificationLink(@PathVariable String userId) throws Exception {
        return ApiResponse.<String>builder()
                .result(chatService.getDailyMetricsAdvice(dailyMetricsService.getLatestDailyMetrics(userId), "Tinh trang suc khoe dang nhu vao", "vi"))
                .build();
    }
}
