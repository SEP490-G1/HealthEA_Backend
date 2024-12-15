package com.example.identity_service.service;

import com.example.identity_service.dto.reponse.UserResponse;
import com.example.identity_service.dto.request.ChangePassRequest;
import com.example.identity_service.dto.request.UserCreationRequest;
import com.example.identity_service.dto.request.UserUpdateRequest;
import com.example.identity_service.entity.Doctor;
import com.example.identity_service.entity.User;
import com.example.identity_service.enums.Role;
import com.example.identity_service.enums.Status;
import com.example.identity_service.exception.AppException;
import com.example.identity_service.exception.ErrorCode;
import com.example.identity_service.mapper.UserMapper;
import com.example.identity_service.repository.DoctorRepository;
import com.example.identity_service.repository.UserRepository;
import lombok.AccessLevel;
import lombok.RequiredArgsConstructor;
import lombok.experimental.FieldDefaults;
import lombok.extern.slf4j.Slf4j;
import org.springframework.data.domain.Pageable;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
@RequiredArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE, makeFinal = true)
@Slf4j
public class UserService {
    UserRepository userRepository;
    UserMapper userMapper;
    PasswordEncoder passwordEncoder;
    DoctorRepository doctorReposity;

    public UserResponse createRequest(UserCreationRequest request){
        log.info("Service: Create user: " + request);

        if(userRepository.existsByUsername(request.getUsername()) ){
            throw new AppException(ErrorCode.USER_EXISTED);
        }else if(userRepository.existsByEmail(request.getEmail())){
            throw new AppException(ErrorCode.EMAIL_EXISTED);
        }else if(userRepository.existsByPhone(request.getEmail())){
            throw new AppException(ErrorCode.PHONE_EXISTED);
        }

        request.setPassword(passwordEncoder.encode(request.getPassword()));
        if(request.getRole() == null){
            request.setRole(Role.CUSTOMER);
        }
        request.setStatus(Status.INACTIVE);

        User user = userMapper.toUser(request);

        UserResponse userResponse = userMapper.toUserResponse(userRepository.save(user));

        if(user.getRole().equals(Role.DOCTOR)){
            Doctor doctor = new Doctor();
            doctor.setUserId(userResponse.getId());
            doctor.setDisplayName(userResponse.getFirstName() + " " + userResponse.getLastName());
            doctorReposity.save(doctor);
        }

        return userResponse;
    }

    //    @PreAuthorize("hasRole('ADMIN')") //có role admin mới vào hàm này
//    @PreAuthorize("hasRole('Admin')")
    public List<UserResponse> getUser(Pageable pageable){
        log.info("In method get Users");
        return userRepository.findAll(pageable).stream().map(userMapper::toUserResponse).toList();
    }

    public UserResponse getMyInfo(){
        var context = SecurityContextHolder.getContext();
        String name = context.getAuthentication().getName();
        return userMapper.toUserResponse(userRepository.findByUsername(name).orElseThrow(() -> new AppException(ErrorCode.USER_NOT_EXISTED)));
    }

    //    @PostAuthorize("returnObject.username == authentication.name") //in charge sau khi method done
    public UserResponse getUser(String id){
        log.info("In method get user by id");
        return userMapper.toUserResponse(userRepository.findById(id).orElseThrow(() -> new AppException(ErrorCode.USER_NOT_EXISTED)));
    }

    public UserResponse updateUser(String userId, UserUpdateRequest request){
        User user = userRepository.findById(userId).orElseThrow(() -> new AppException(ErrorCode.USER_NOT_EXISTED));

        user.setEmail(request.getEmail());
        user.setPhone(request.getPhone());
        user.setFirstName(request.getFirstName());
        user.setLastName(request.getLastName());
        user.setDob(request.getDob());
        user.setGender(request.getGender());
        user.setPhone(request.getPhone());
        user.setAvatar(request.getAvatar());
        if(request.getStatus() == null){
            user.setStatus(Status.ACTIVE);
        }else {
            user.setStatus(request.getStatus());
        }

        return userMapper.toUserResponse(userRepository.save(user));
    }

    public UserResponse changePassword(String userId, ChangePassRequest request){
        User user = userRepository.findById(userId).orElseThrow(() -> new AppException(ErrorCode.USER_NOT_EXISTED));

        boolean authenticated = passwordEncoder.matches(request.getOldPass(), user.getPassword());
        if(authenticated){
            user.setPassword(passwordEncoder.encode(request.getNewPass()));
            return userMapper.toUserResponse(userRepository.save(user));
        }else {
            throw new AppException(ErrorCode.OLD_PASSWORD_INVALID);
        }
    }

    public void deleteUser(String userId){
        userRepository.deleteById(userId);
    }

    public List<UserResponse> searchUsers(String username, String email, String status, String role) {
        return userRepository.findUsers(username, email, status, role).stream().map(userMapper::toUserResponse).toList();
    }

    public Long getTotalUser() {
        return userRepository.count();
    }

    public List<String> getAllUsername(){
        return userRepository.findAllUsername();
    }

}

