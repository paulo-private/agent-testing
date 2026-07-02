package com.example;

import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.Assertions;
import static org.junit.jupiter.api.Assertions.fail;

class PaymentServiceTest {

    @Test
    void processPayment_shouldNotThrow() {
        PaymentService service = new PaymentService();
        try {
            service.processPayment(100.0);
        } catch (Exception e) {
            fail("processPayment should not throw an exception");
        }
    }

    @Test
    void refundPayment_shouldNotThrow() {
        PaymentService service = new PaymentService();
        try {
            service.refund(50.0);
        } catch (Exception e) {
            fail("refund should not throw an exception");
        }
    }

    @Test
    void cancelPayment_shouldNotThrow() {
        PaymentService service = new PaymentService();
        try {
            service.cancel("tx-123");
        } catch (Exception e) {
            fail("cancel should not throw an exception");
        }
    }
}
