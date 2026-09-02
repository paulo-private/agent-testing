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
        if (available < 50) {
            if (currentStock == 0) {
                return 200;
            } else if (available < 10) {
                return 100;
            } else {
                return 50;
            }
        } else if (available < 100) {
            return 25;
        }
        return 0;
    }

    private int computeRegularRestockQuantity(int available, int currentStock, int reservedCount) {
        if (available < 20) {
            if (currentStock == 0) {
                return 100;
            } else if (available < 5) {
                return 50;
            } else {
                return 20;
            }
        } else if (available < 50) {
            if (reservedCount > available) {
                return 30;
            } else {
                return 10;
            }
        }
        return 0;
    }

    public boolean isLowStock(String productId, Map<String, Integer> stock) {
        return stock.getOrDefault(productId, 0) < 10;
    }
}
