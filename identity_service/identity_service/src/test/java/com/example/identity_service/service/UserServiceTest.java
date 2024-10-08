package com.example.identity_service.service;

import com.example.identity_service.dto.reponse.UserResponse;
import com.example.identity_service.dto.request.UserCreationRequest;
import com.example.identity_service.entity.User;
import com.example.identity_service.exception.AppException;
import com.example.identity_service.repository.UserRepository;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.fasterxml.jackson.datatype.jsr310.JavaTimeModule;
import lombok.extern.slf4j.Slf4j;
import org.assertj.core.api.Assertions;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.mockito.ArgumentMatchers;
import org.mockito.Mockito;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.AutoConfigureMockMvc;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.boot.test.mock.mockito.MockBean;

import java.time.LocalDate;

import static org.junit.jupiter.api.Assertions.assertThrows;

@Slf4j
@SpringBootTest
@AutoConfigureMockMvc
public class UserServiceTest {
    @Autowired
    private UserService userService;

    @MockBean
    private UserRepository userRepository;

    private UserCreationRequest request;
    private UserResponse userResponse;
    private User user;
    private LocalDate dob;

    @BeforeEach
        //call before start any test
    void initData(){
        dob = LocalDate.of(2003, 12, 23);

        request = UserCreationRequest.builder()
                .username("test12")
                .firstName("duong")
                .lastName("nguyen")
                .password("12345678")
                .dob(dob)
                .build();

        userResponse = UserResponse.builder()
                .id("b200b67c9849")
                .username("test12")
                .firstName("duong")
                .lastName("nguyen")
                .dob(dob)
                .build();

        user = User.builder()
                .id("b200b67c9849")
                .username("test12")
                .firstName("duong")
                .lastName("nguyen")
                .dob(dob)
                .build();
    }

    @Test
    void createUser_validRequest_success() throws Exception {
        //GIVEN

        Mockito.when(userRepository.existsByUsername(ArgumentMatchers.anyString())).thenReturn(false);
        Mockito.when(userRepository.save(ArgumentMatchers.any())).thenReturn(user);

        //WHEN
        var response = userService.createRequest(request);

        //THEN
        Assertions.assertThat(response.getId()).isEqualTo("b200b67c9849");
        Assertions.assertThat(response.getUsername()).isEqualTo("test12");
    }

    @Test
    void createUser_usernameExisted_fail() throws Exception {
        //GIVEN
        request.setUsername("admin");
        Mockito.when(userRepository.existsByUsername(ArgumentMatchers.anyString())).thenReturn(true);

        //WHEN
        var exception = assertThrows(AppException.class,() -> userService.createRequest(request));

        //THEN
        Assertions.assertThat(exception.getErrorCode().getCode()).isEqualTo(1001);
    }
}
