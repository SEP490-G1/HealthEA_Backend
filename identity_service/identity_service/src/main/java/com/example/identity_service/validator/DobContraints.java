package com.example.identity_service.validator;

import jakarta.validation.Constraint;
import jakarta.validation.Payload;
import jakarta.validation.constraints.Size;

import java.lang.annotation.*;

@Target({ElementType.FIELD}) //sử dụng ở đâu
@Retention(RetentionPolicy.RUNTIME) //xử lý lúc nào
@Constraint(
        validatedBy = { DobValidator.class}
) //class chịu trách nhiệm validate
public @interface DobContraints {
    String message() default "Invalid date of birth"; //message

    int min();

    Class<?>[] groups() default {};

    Class<? extends Payload>[] payload() default {};
}
