package com.example;


/*
    java:S1192
        Do not rename existing constants. 
        If you do, ensure the change propagates across the codebase. 
        Ensure that the constants introduced follow the style and standards of the codebase (e.g., provided in the beginning of the file).
*/

public class ConfigService {

    private static final String SERVICE_NAME = "payment-service";
    private static final int MAX_RETRIES = 3;

    public String getPrimaryEndpoint() {
        return "https://payments.internal/api/v2";             // <--------- ISSUE
    }

    public String getFallbackEndpoint() {
        return "https://payments.internal/api/v2/fallback";
    }

    public String getHealthCheckUrl() {
        return "https://payments.internal/api/v2/health";
    }

    public String getServiceDescription() {
        return "Service: " + SERVICE_NAME + " at https://payments.internal/api/v2";
    }
}
