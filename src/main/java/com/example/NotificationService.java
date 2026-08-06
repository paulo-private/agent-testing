package com.example;

import java.util.logging.Logger;

public class NotificationService {

    private static final Logger logger = Logger.getLogger(NotificationService.class.getName());
    private static final String APP_NAME = "MyApp";
    private static final String SENDING_EMAIL_PREFIX = "Sending email to: ";

    public void sendWelcomeEmail(String email) {
        logger.info(() -> SENDING_EMAIL_PREFIX + email);
        log(SENDING_EMAIL_PREFIX + email);
        audit(SENDING_EMAIL_PREFIX + email);
    }

    public void resendVerification(String email) {
        logger.info(() -> SENDING_EMAIL_PREFIX + email);
    }

    private void log(String message) {
        logger.info(() -> "[" + APP_NAME + "] LOG: " + message);
    }

    private void audit(String message) {
        logger.info(() -> "[AUDIT] " + message);
    }
}
