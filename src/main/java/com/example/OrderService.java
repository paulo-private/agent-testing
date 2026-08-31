package com.example;

import java.io.IOException;
import java.sql.SQLException;

public class OrderService {

    public void processOrders() throws IOException, SQLException {
        if (!loadOrders()) {
            throw new IOException("Failed to load orders from disk");
        }
    }

    private boolean loadOrders() {
        try {
            // Attempt to load orders from disk
            return true;
        } catch (Exception e) {
            return false;
        }
    }

    public String fetchStatus() {
        try {
            processOrders();
        } catch (IOException | SQLException e) {
            return "error: " + e.getMessage();
        }
        return "ok";
    }
}
