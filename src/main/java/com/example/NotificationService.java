package com.example;

import java.util.logging.Logger;

public class NotificationService {

    private static final Logger LOGGER = Logger.getLogger(NotificationService.class.getName());

    private static final String APP_NAME = "MyApp";
    private static final String SENDING_EMAIL_TO = "Sending email to: ";

    public void sendWelcomeEmail(String email) {
        LOGGER.info(() -> SENDING_EMAIL_TO + email);
        log(SENDING_EMAIL_TO + email);
        audit(SENDING_EMAIL_TO + email);
    }

    public void resendVerification(String email) {
        LOGGER.info(() -> SENDING_EMAIL_TO + email);
    }

    private void log(String message) {
        System.out.println("[" + APP_NAME + "] LOG: " + message);
    }

    private void audit(String message) {
        System.out.println("[AUDIT] " + message);
    }
}
