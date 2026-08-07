package com.example;

import java.util.logging.Level;
import java.util.logging.Logger;

public class NotificationService {

    private static final Logger logger = Logger.getLogger(NotificationService.class.getName());
    private static final String APP_NAME = "MyApp";

    public void sendWelcomeEmail(String email) {
        logger.log(Level.INFO, "Sending email to: {0}", email);
        log("Sending email to: " + email);
        audit("Sending email to: " + email);
    }

    public void resendVerification(String email) {
        logger.log(Level.INFO, "Sending email to: {0}", email);
    }

    private void log(String message) {
        logger.log(Level.INFO, "[{0}] LOG: {1}", new Object[]{APP_NAME, message});
    }

    private void audit(String message) {
        logger.log(Level.INFO, "[AUDIT] {0}", message);
    }
}
