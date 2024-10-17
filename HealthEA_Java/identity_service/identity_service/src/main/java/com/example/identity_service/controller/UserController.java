package com.example.identity_service.controller;

import com.example.identity_service.dto.reponse.UserResponse;
import com.example.identity_service.dto.request.*;
import com.example.identity_service.service.UserService;
import jakarta.validation.Valid;
import lombok.AccessLevel;
import lombok.RequiredArgsConstructor;
import lombok.experimental.FieldDefaults;
import lombok.extern.slf4j.Slf4j;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Pageable;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@CrossOrigin(origins = "http://localhost:5173/")
@RestController
@RequestMapping("/users")
@RequiredArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE, makeFinal = true)
@Slf4j
public class UserController {
    UserService userService;

    @PostMapping
    ApiResponse<UserResponse> createUser(@RequestBody @Valid UserCreationRequest request){ //auto map request vào với dto
        log.info("Controller: Create user");
        ApiResponse<UserResponse> apiResponse = new ApiResponse<>();

        apiResponse.setResult(userService.createRequest(request));
        return apiResponse;
    }

    @GetMapping
    ApiResponse<List<UserResponse>> getUsers( @RequestParam(defaultValue = "0") int page,
                                              @RequestParam(defaultValue = "10") int size ){
        var authentication = SecurityContextHolder.getContext().getAuthentication();
        Pageable pageable = PageRequest.of(page, size);

        log.info("Username: {}" , authentication.getName());
        authentication.getAuthorities().forEach(grantedAuthority -> log.info(grantedAuthority.getAuthority()));

        return ApiResponse.<List<UserResponse>>builder()
                .result(userService.getUser(pageable)).build();
    }

    @GetMapping("/total")
    ApiResponse<Long> getTotalUser( ){
        return ApiResponse.<Long>builder()
                .result(userService.getTotalUser()).build();
    }

    @GetMapping("/{userId}")
    ApiResponse<UserResponse> getUser(@PathVariable String userId){ //map paramater vào method
        return ApiResponse.<UserResponse>builder()
                .result(userService.getUser(userId))
                .build();
    }

    @GetMapping("/myinfo")
    ApiResponse<UserResponse> getMyInfo(){ //map paramater vào method
        return ApiResponse.<UserResponse>builder()
                .result(userService.getMyInfo())
                .build();
    }

    @PutMapping("/{userId}")
    ApiResponse<UserResponse> updateUser(@PathVariable String userId, @RequestBody UserUpdateRequest request){
        return ApiResponse.<UserResponse>builder()
                .result(userService.updateUser(userId, request))
                .build();
    }

    @DeleteMapping("/{userId}")
    ApiResponse<String> deleteUser(@PathVariable String userId){
        userService.deleteUser(userId);
        return ApiResponse.<String>builder().result("User has been deleted").build();
    }

    @PutMapping("/changePass/{userId}")
    ApiResponse<UserResponse> changeUserPassword(@PathVariable String userId, @RequestBody ChangePassRequest request){
        return ApiResponse.<UserResponse>builder()
                .result(userService.changePassword(userId, request))
                .build();
    }

    @GetMapping("/search")
    public ApiResponse<List<UserResponse>> searchUsers(
            @RequestParam(required = false) String username,
            @RequestParam(required = false) String email,
            @RequestParam(required = false) String status,
            @RequestParam(required = false) String role) {
        return ApiResponse.<List<UserResponse>>builder()
                .result(userService.searchUsers(username, email, status, role))
                .build();
    }

    @GetMapping("/username")
    public ApiResponse<List<String>> getAllUsername() {
        return ApiResponse.<List<String>>builder()
                .result(userService.getAllUsername())
                .build();
    }
}
