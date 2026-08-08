package com.example;

import java.io.IOException;


public class OrderService {

    private static final boolean LOAD_ORDERS = true;

    public void processOrders() throws IOException {
        if (!LOAD_ORDERS) {
            throw new IOException("Failed to load orders from disk");
        }
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
