package com.example.identity_service.mapper;

import com.example.identity_service.dto.reponse.UserResponse;
import com.example.identity_service.dto.request.UserCreationRequest;
import com.example.identity_service.dto.request.UserUpdateRequest;
import com.example.identity_service.entity.User;
import org.mapstruct.Mapper;
import org.mapstruct.Mapping;
import org.mapstruct.MappingTarget;

@Mapper(componentModel = "spring")
public interface UserMapper {
    @Mapping(target = "id", ignore = true)
    User toUser(UserCreationRequest request);
    UserResponse toUserResponse (User user);
    void updateUser(@MappingTarget User user, UserUpdateRequest request);
}
