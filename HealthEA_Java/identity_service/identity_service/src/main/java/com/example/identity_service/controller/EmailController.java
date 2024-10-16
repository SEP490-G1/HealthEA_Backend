package com.example.identity_service.controller;

import com.example.identity_service.dto.request.ApiResponse;
import com.example.identity_service.entity.User;
import com.example.identity_service.enums.Status;
import com.example.identity_service.exception.AppException;
import com.example.identity_service.exception.ErrorCode;
import com.example.identity_service.repository.UserRepository;
import com.example.identity_service.service.EmailService;
import com.example.identity_service.service.OTPService;
import lombok.AccessLevel;
import lombok.RequiredArgsConstructor;
import lombok.experimental.FieldDefaults;
import lombok.extern.slf4j.Slf4j;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

@CrossOrigin(origins = "http://localhost:5173/")
@RestController
@RequestMapping("/emails")
@RequiredArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE, makeFinal = true)
@Slf4j
public class EmailController {
    @Autowired
    EmailService emailService;
    @Autowired
    OTPService otpService;
    @Autowired
    UserRepository userRepository;

    @PostMapping("/sendVerifyEmail/{email}")
    public ApiResponse<String> sendVerificationLink(@PathVariable String email) {
        log.info("Email of user: " + email);
        String token = otpService.createVerificationToken(email);
        log.info("Token sent: " + token);
        emailService.sendVerificationEmail(email, token);

        return ApiResponse.<String>builder()
                .result("Verification link has been sent to your email.")
                .build();
    }

    @PostMapping("/verifyEmail/{token}")
    public ApiResponse<String> verifyEmail(@PathVariable String token) {
        log.info("Token received: " + token);
        String email = otpService.getEmailFromToken(token);

        if (email == null) {
            return ApiResponse.<String>builder()
                    .result("Verification link has expired or is invalid.")
                    .build();
        }

        log.info("Email user verify token: " + email);

        User user = userRepository.findByEmail(email).orElseThrow(() -> new AppException(ErrorCode.USER_NOT_EXISTED));
        user.setStatus(Status.ACTIVE);
        userRepository.save(user);


        otpService.removeToken(token);

        return ApiResponse.<String>builder()
                .result("Email has been verified successfully.")
                .build();
    }
}
