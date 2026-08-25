package com.example;

public class NotificationService {
    private static final String EMAIL_RECIPIENT_MESSAGE_PREFIX = "Sending email to: ";

    private static final String APP_NAME = "MyApp";

    public void sendWelcomeEmail(String email) {
        System.out.println(EMAIL_RECIPIENT_MESSAGE_PREFIX + email);
        log(EMAIL_RECIPIENT_MESSAGE_PREFIX + email);
        audit(EMAIL_RECIPIENT_MESSAGE_PREFIX + email);
    }

    public void resendVerification(String email) {
        System.out.println(EMAIL_RECIPIENT_MESSAGE_PREFIX + email);
    }

    private void log(String message) {
        System.out.println("[" + APP_NAME + "] LOG: " + message);
    }

    private void audit(String message) {
        System.out.println("[AUDIT] " + message);
    }
}
