package com.example.identity_service.repository;

import com.example.identity_service.entity.Doctor;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface DoctorReposity extends JpaRepository<Doctor, String> {
}
