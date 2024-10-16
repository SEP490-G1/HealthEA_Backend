package com.example.identity_service.exception;

import lombok.AccessLevel;
import lombok.Getter;
import lombok.experimental.FieldDefaults;
import org.springframework.http.HttpStatus;
import org.springframework.http.HttpStatusCode;

@Getter
@FieldDefaults(level = AccessLevel.PRIVATE, makeFinal = true)
public enum ErrorCode {
    USER_EXISTED (1001, "Username existed", HttpStatus.BAD_REQUEST ),
    USER_NOT_EXISTED (1005, "User not existed", HttpStatus.NOT_FOUND),
    UNCATEGORIED_EXCEPTION (1002, "Uncategorized error", HttpStatus.INTERNAL_SERVER_ERROR),
    USERNAME_INVALID (1003, "Username must have at least {min} characters", HttpStatus.BAD_REQUEST),
    PASSWORD_INVALID (1004, "Password must have at least {min} characters", HttpStatus.BAD_REQUEST),
    KEY_INVALID (1007, "Invalid message key", HttpStatus.BAD_REQUEST),
    UNAUTHENTICATED (1006, "Unauthenticated", HttpStatus.UNAUTHORIZED),
    UNAUTHORIZED (1008, "You do not have permission", HttpStatus.FORBIDDEN),
    DOB_INVALID (1009, "Your age must be at least {min}", HttpStatus.BAD_REQUEST),
    EMAIL_EXISTED (1010, "Email existed", HttpStatus.BAD_REQUEST ),
    PHONE_EXISTED (1011, "Username existed", HttpStatus.BAD_REQUEST ),
    OLD_PASSWORD_INVALID (1012, "Your current password is invalid", HttpStatus.BAD_REQUEST),
    UNIT_NUMBER_EXISTED (1013, "Unit number existed", HttpStatus.BAD_REQUEST ),
    APARTMENT_NOT_EXISTED (1014, "Apartment not existed", HttpStatus.BAD_REQUEST ),
    DUE_DATE_VALID(1015, "Due date must be greater than created date", HttpStatus.BAD_REQUEST ),
    BILL_DUPLICATED(1016, "This type of bill already existed for this aparment in this month" , HttpStatus.BAD_REQUEST),
    BILL_NOT_EXISTED(1017, "Bill not existed" , HttpStatus.BAD_REQUEST),
    RESIDENT_NOT_EXISTED(1018, "Resident not existed", HttpStatus.BAD_REQUEST),
    CONTRACT_NOT_EXISTED(1019, "Contract not existed", HttpStatus.BAD_REQUEST),
    USER_NOT_VERIFIED(1020, "Your email has not been verified. Please verify DUONGTEST", HttpStatus.BAD_REQUEST);

    int code;
    String messgae;
    HttpStatusCode statusCode;

    ErrorCode(int code, String messgae, HttpStatusCode statusCode){
        this.code = code;
        this.messgae = messgae;
        this.statusCode = statusCode;
    }

}
