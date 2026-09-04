package com.example;

import java.io.IOException;
import java.sql.SQLException;

public class OrderService {

    public void processOrders() throws IOException, SQLException {
        loadOrders();
    }

    private boolean loadOrders() {
        return true;
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
