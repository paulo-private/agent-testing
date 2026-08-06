package com.example;

import java.util.List;
import java.util.Map;

public class InventoryService {

    public int computeRestockQuantity(String productId, Map<String, Integer> stock,
                                      Map<String, Integer> reserved, List<String> vipProducts) {
        int available = stock.getOrDefault(productId, 0) - reserved.getOrDefault(productId, 0);
        if (available < 0) {
            available = 0;
        }
        if (vipProducts.contains(productId)) {
            return computeVipRestockQuantity(available, stock.getOrDefault(productId, 0));
        }
        return computeRegularRestockQuantity(available, stock.getOrDefault(productId, 0),
                                             reserved.getOrDefault(productId, 0));
    }

    private int computeVipRestockQuantity(int available, int currentStock) {
        if (available >= 100) {
            return 0;
        }
        if (available >= 50) {
            return 25;
        }
        if (currentStock == 0) {
            return 200;
        }
        if (available < 10) {
            return 100;
        }
        return 50;
    }

    private int computeRegularRestockQuantity(int available, int currentStock, int currentReserved) {
        if (available >= 50) {
            return 0;
        }
        if (available >= 20) {
            return currentReserved > available ? 30 : 10;
        }
        if (currentStock == 0) {
            return 100;
        }
        if (available < 5) {
            return 50;
        }
        return 20;
    }

    public boolean isLowStock(String productId, Map<String, Integer> stock) {
        return stock.getOrDefault(productId, 0) < 10;
    }
}
