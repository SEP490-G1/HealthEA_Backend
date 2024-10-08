package com.example.identity_service.service;

import com.google.common.cache.Cache;
import com.google.common.cache.CacheBuilder;
import lombok.AccessLevel;
import lombok.experimental.FieldDefaults;
import org.springframework.stereotype.Service;

import java.util.UUID;
import java.util.concurrent.TimeUnit;

@Service
@FieldDefaults(level = AccessLevel.PRIVATE, makeFinal = true)
public class OTPService {

    Cache<String, String> tokenCache = CacheBuilder.newBuilder()
            .expireAfterWrite(100, TimeUnit.MINUTES)
            .build();

    public String createVerificationToken(String email) {
        String token = UUID.randomUUID().toString();
        tokenCache.put(token, email);
        return token;
    }

    public String getEmailFromToken(String token) {
        return tokenCache.getIfPresent(token);
    }

    public void removeToken(String token) {
        tokenCache.invalidate(token);
    }
}

