package com.example.identity_service.service;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.mail.SimpleMailMessage;
import org.springframework.mail.javamail.JavaMailSender;
import org.springframework.stereotype.Service;

@Service
public class EmailService {

    @Autowired
    private JavaMailSender javaMailSender;

    public void sendVerificationEmail(String toUser, String token) {
        String verifyUrl = "http://localhost:5173/client/verify?token=" + token;

        SimpleMailMessage message = new SimpleMailMessage();
        message.setTo(toUser);
        message.setSubject("Verify your email address");
        message.setText("Please click the following link to verify your email address: " + verifyUrl);
        message.setFrom("duongnthn2312@gmail.com");

        javaMailSender.send(message);
        System.out.println("Verification email sent successfully!");
    }
}

