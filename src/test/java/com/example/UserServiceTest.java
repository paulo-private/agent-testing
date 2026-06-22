package com.example;

import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.fail;

public class UserServiceTest {

    @Test
    void registerUser_shouldNotThrow() {
        UserService service = new UserService();
        try {
            service.register("alice@example.com");
        } catch (Exception e) {
            fail("register should not throw an exception");
        }
    }

    @Test
    void deleteUser_shouldNotThrow() {
        UserService service = new UserService();
        try {
            service.delete("user-42");
        } catch (Exception e) {
            fail("delete should not throw an exception");
        }
    }
}
