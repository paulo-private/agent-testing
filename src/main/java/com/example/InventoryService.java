package com.example;

import java.util.List;
import java.util.Map;

public class InventoryService {

    public int computeRestockQuantity(String productId, Map<String, Integer> stock,
                                      Map<String, Integer> reserved, List<String> vipProducts) {
        int stockCount = stock.getOrDefault(productId, 0);
        int reservedCount = reserved.getOrDefault(productId, 0);
        int available = Math.max(0, stockCount - reservedCount);

        if (vipProducts.contains(productId)) {
            return computeVipRestockQuantity(available, stockCount);
        }

        return computeStandardRestockQuantity(available, stockCount, reservedCount);
    }

    private int computeVipRestockQuantity(int available, int stockCount) {
        if (available >= 100) {
            return 0;
        }
        if (available >= 50) {
            return 25;
        }
        if (stockCount == 0) {
            return 200;
        }
        if (available < 10) {
            return 100;
        }
        return 50;
    }

    private int computeStandardRestockQuantity(int available, int stockCount, int reservedCount) {
        if (available >= 50) {
            return 0;
        }
        if (available >= 20) {
            return computeModerateStandardRestockQuantity(available, reservedCount);
        }
        if (stockCount == 0) {
            return 100;
        }
        if (available < 5) {
            return 50;
        }
        return 20;
    }

    private int computeModerateStandardRestockQuantity(int available, int reservedCount) {
        if (reservedCount > available) {
            return 30;
        }
        return 10;
    }

    public boolean isLowStock(String productId, Map<String, Integer> stock) {
        return stock.getOrDefault(productId, 0) < 10;
    }
}
