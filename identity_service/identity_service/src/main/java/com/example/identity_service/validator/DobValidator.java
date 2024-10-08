package com.example.identity_service.validator;

import jakarta.validation.ConstraintValidator;
import jakarta.validation.ConstraintValidatorContext;

import java.time.LocalDate;
import java.time.temporal.ChronoUnit;
import java.util.Objects;

public class DobValidator implements ConstraintValidator<DobContraints, LocalDate> { //annotations, kiểu dữ liệu
    private int min;

    //init, get thông số của annotations. VD: min của dob
    @Override
    public void initialize(DobContraints constraintAnnotation) {
        ConstraintValidator.super.initialize(constraintAnnotation);
        min = constraintAnnotation.min();
    }

    //validate
    @Override
    public boolean isValid(LocalDate value, ConstraintValidatorContext constraintValidatorContext) {
        if (Objects.isNull(value)){
            return true;
        }

        long year = ChronoUnit.YEARS.between(value, LocalDate.now());

        return (year >= min);
    }
}
