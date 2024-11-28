package com.example.identity_service.entity;

import com.example.identity_service.enums.SenderType;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import jakarta.persistence.*;
import java.util.UUID;

@Entity
@Table(name = "chatMessage")
@Data
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class ChatMessage {

    @Id
    @GeneratedValue(strategy = GenerationType.UUID)
    @Column(name = "messageId", nullable = false)
    private String messageId;

    @Column(name = "userId", nullable = false)
    private String userId;  // Liên kết với bảng User thông qua userId

    @Enumerated(EnumType.STRING)
    @Column(name = "senderType", nullable = false)
    private SenderType senderType;  // Enum cho kiểu người gửi (AI, USER)

    @Column(name = "message", nullable = false, columnDefinition = "TEXT")
    private String message;

    @Column(name = "created_at", nullable = false)
    private java.sql.Timestamp createdAt;

    @PrePersist
    public void prePersist() {
        this.createdAt = new java.sql.Timestamp(System.currentTimeMillis());
    }


}

