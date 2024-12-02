package com.example.identity_service.controller;

import com.example.identity_service.dto.reponse.DailyMetricsResponse;
import com.example.identity_service.dto.request.ApiResponse;
import com.example.identity_service.entity.ChatMessage;
import com.example.identity_service.enums.SenderType;
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

import java.util.List;

@CrossOrigin(origins = "http://localhost:5173/")
@RestController
@RequestMapping("/chat")
@FieldDefaults(level = AccessLevel.PRIVATE, makeFinal = true)
@Slf4j

public class ChatController {

    private final ChatService chatService;
    private final DailyMetricsService dailyMetricsService;

    public ChatController( ChatService chatService, DailyMetricsService dailyMetricsService) {
        this.chatService = chatService;
        this.dailyMetricsService = dailyMetricsService;
    }

    @PostMapping("/getAiResponse")
    public ApiResponse<String> sendQuestion(@RequestBody String question) throws Exception {
        chatService.saveMessage(question, SenderType.USER);
        String aiRsp = chatService.getDailyMetricsAdvice(dailyMetricsService.getLatestDailyMetrics(), question, "vi");
        chatService.saveMessage(aiRsp, SenderType.AI);
        return ApiResponse.<String>builder()
                .result(aiRsp)
                .build();
    }

    @GetMapping()
    public ApiResponse<List<ChatMessage>> getMessagesByUserId() {
        return ApiResponse.<List<ChatMessage>>builder()
                .result(chatService.getMessagesByUserId())
                .build();
    }
}
