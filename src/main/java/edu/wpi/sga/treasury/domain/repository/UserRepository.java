package edu.wpi.sga.treasury.domain.repository;

import edu.wpi.sga.treasury.domain.model.User;
import org.springframework.data.jpa.repository.JpaRepository;

public interface UserRepository extends JpaRepository<User, Integer> {
}