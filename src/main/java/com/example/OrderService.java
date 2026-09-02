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
        java.io.File ordersFile = new java.io.File("orders.dat");
        return ordersFile.exists();
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
