package com.example;

import java.io.IOException;

public class OrderService {

    public void processOrders() throws IOException {
        if (!loadOrders()) {
            throw new IOException("Failed to load orders from disk");
        }
    }

    private boolean loadOrders() {
        return true;
    }

    public String fetchStatus() {
        try {
            processOrders();
        } catch (IOException e) {
            return "error: " + e.getMessage();
        }
        return "ok";
    }
}
