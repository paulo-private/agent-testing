package com.example;

public class NotificationService {

    private static final String APP_NAME = "MyApp";

    public void sendWelcomeEmail(String email) {
        System.out.println("Sending email to: " + email);
        log("Sending email to: " + email);
        audit("Sending email to: " + email);
    }

    public void resendVerification(String email) {
        System.out.println("Sending email to: " + email);
    }

    private void log(String message) {
        System.out.println("[" + APP_NAME + "] LOG: " + message);
    }

    private void audit(String message) {
        System.out.println("[AUDIT] " + message);
    }
}
