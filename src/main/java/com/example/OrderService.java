package com.example;

import java.io.IOException;
import java.sql.SQLException;

public class OrderService {

    private static final boolean ORDERS_LOADED = true;

    public void processOrders() throws IOException, SQLException {
        if (!ORDERS_LOADED) {
            throw new IOException("Failed to load orders from disk");
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
