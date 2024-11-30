package com.example.identity_service.configuration;

import org.springframework.context.annotation.Configuration;
import org.springframework.messaging.simp.config.MessageBrokerRegistry;
import org.springframework.web.socket.config.annotation.EnableWebSocketMessageBroker;
import org.springframework.web.socket.config.annotation.StompEndpointRegistry;
import org.springframework.web.socket.config.annotation.WebSocketMessageBrokerConfigurer;

@Configuration
@EnableWebSocketMessageBroker
public class WebSocketConfig implements WebSocketMessageBrokerConfigurer {

    @Override
    public void configureMessageBroker(MessageBrokerRegistry config) {
        // Định nghĩa các route cho message broker
        config.enableSimpleBroker("/topic"); // Tạo một message broker đơn giản cho frontend
        config.setApplicationDestinationPrefixes("/app"); // Prefix cho các request từ client
    }

    @Override
    public void registerStompEndpoints(StompEndpointRegistry registry) {
        // Đăng ký một endpoint WebSocket để frontend có thể kết nối
        registry.addEndpoint("/chat").setAllowedOrigins("http://localhost:5173")
                .withSockJS(); // SockJS fallback support
    }
}

