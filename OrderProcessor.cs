namespace Example;

public class Order
{
    public string? Type { get; set; }
    public double Amount { get; set; }
    public string? Priority { get; set; }
    public bool IsRenewal { get; set; }
    public string? DiscountCode { get; set; }
    public int Quantity { get; set; }
}

public class OrderProcessor
{
    public string ProcessOrder(Order order)
    {
        if (order == null)
            return "invalid";

        if (order.Amount <= 0)
            return "invalid_amount";

        string result;
        switch (order.Type)
        {
            case "standard":
                if (order.Amount > 1000 && order.Priority == "high")
                    result = "express";
                else if (order.Amount > 500)
                    result = "priority";
                else
                    result = "normal";
                break;
            case "subscription":
                if (order.IsRenewal && order.DiscountCode != null)
                    result = "discounted_renewal";
                else if (order.IsRenewal)
                    result = "renewal";
                else
                    result = "new_subscription";
                break;
            case "bulk":
                if (order.Quantity > 100 || (order.Quantity > 50 && order.Priority == "high"))
                    result = "bulk_express";
                else
                    result = "bulk_standard";
                break;
            default:
                result = "unknown";
                break;
        }

        return result;
    }
}
