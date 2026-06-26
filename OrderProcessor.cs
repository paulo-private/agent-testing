namespace Example;

public class OrderItem
{
    public string ProductId { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public bool IsHazmat { get; set; }
    public bool IsFragile { get; set; }
    public bool IsOversized { get; set; }
}

public class CustomerInfo
{
    public string Id { get; set; } = string.Empty;
    public bool IsVip { get; set; }
    public int RiskScore { get; set; }
    public string Region { get; set; } = string.Empty;
}

public class ShipmentDestination
{
    public string CountryCode { get; set; } = string.Empty;
    public bool IsInternational { get; set; }
    public bool IsRestrictedZone { get; set; }
}

public class ShipmentOrder
{
    public string CustomerId { get; set; } = string.Empty;
    public string ShippingTier { get; set; } = string.Empty;
    public string? DiscountCode { get; set; }
    public double Weight { get; set; }
    public bool IsOversized { get; set; }
    public List<OrderItem> Items { get; set; } = new();
    public CustomerInfo Customer { get; set; } = new();
    public ShipmentDestination Destination { get; set; } = new();
}

public class ShipmentResult
{
    public string Carrier { get; set; } = string.Empty;
    public int DeliveryDays { get; set; }
    public decimal BasePrice { get; set; }
    public decimal ShippingCost { get; set; }
    public decimal HandlingFee { get; set; }
    public decimal Discount { get; set; }
    public decimal Tax { get; set; }
    public decimal TotalPrice { get; set; }
    public bool RequiresSignature { get; set; }
    public bool RequiresApproval { get; set; }
}

public class ShipmentProcessor
{
    public ShipmentResult ProcessShipmentOrder(ShipmentOrder order)
    {
        ValidateOrder(order);

        var (carrier, deliveryDays) = DetermineCarrier(order);
        decimal basePrice = order.Items.Sum(i => i.Price * i.Quantity);
        decimal shippingCost = CalculateShippingCost(carrier, basePrice, order.Weight);
        decimal discount = CalculateDiscount(order, basePrice, shippingCost);
        decimal taxRate = DetermineTaxRate(order.Destination);
        decimal tax = (basePrice - discount + shippingCost) * taxRate;
        var (handlingFee, requiresSignature) = CalculateHandlingFees(order.Items, carrier);
        bool requiresApproval = DetermineRequiresApproval(order, basePrice, discount);
        decimal totalPrice = basePrice + shippingCost + handlingFee + tax - discount;

        return new ShipmentResult
        {
            Carrier = carrier,
            DeliveryDays = deliveryDays,
            BasePrice = basePrice,
            ShippingCost = shippingCost,
            HandlingFee = handlingFee,
            Discount = discount,
            Tax = tax,
            TotalPrice = totalPrice,
            RequiresSignature = requiresSignature,
            RequiresApproval = requiresApproval,
        };
    }

    private static void ValidateOrder(ShipmentOrder order)
    {
        if (order == null)
            throw new ArgumentNullException(nameof(order));
        if (order.CustomerId.Length == 0)
            throw new ArgumentException("Order must have a customer.");
        if (order.Items.Count == 0)
            throw new ArgumentException("Order must contain at least one item.");
        if (order.Destination.CountryCode.Length == 0)
            throw new ArgumentException("Order must have a valid destination.");
    }

    private static (string carrier, int deliveryDays) DetermineCarrier(ShipmentOrder order)
    {
        switch (order.ShippingTier)
        {
            case "express":
                if (order.Weight > 30 || order.IsOversized)
                    return ("FREIGHT", 2);
                else if (order.Destination.IsInternational && order.Destination.CountryCode != "CA")
                    return ("INTL_EXPRESS", 3);
                else
                    return ("LOCAL_EXPRESS", 1);
            case "standard":
                if (order.Destination.IsInternational)
                    return ("INTL_STANDARD", 14);
                else if (order.Weight > 50)
                    return ("FREIGHT", 5);
                else
                    return ("STANDARD", 5);
            case "economy":
                return order.Destination.IsInternational
                    ? ("INTL_ECONOMY", 21)
                    : ("GROUND", 7);
            case "white_glove":
                return order.Destination.IsInternational
                    ? ("INTL_PREMIUM", 5)
                    : ("WHITE_GLOVE", 2);
            default:
                return ("STANDARD", 5);
        }
    }

    private static decimal CalculateShippingCost(string carrier, decimal basePrice, double weight)
    {
        if (carrier == "FREIGHT")
            return (decimal)weight * 2.5m;
        else if (carrier == "INTL_EXPRESS" || carrier == "INTL_STANDARD")
            return basePrice * 0.15m + 25m;
        else if (carrier == "INTL_ECONOMY")
            return basePrice * 0.08m + 10m;
        else if (carrier == "INTL_PREMIUM")
            return basePrice * 0.20m + 50m;
        else if (carrier == "WHITE_GLOVE")
            return basePrice * 0.12m + 30m;
        else
            return (decimal)weight * 0.5m + 5m;
    }

    private static decimal CalculateDiscount(ShipmentOrder order, decimal basePrice, decimal shippingCost)
    {
        decimal discount = 0m;
        if (order.Customer.IsVip && basePrice > 500m)
            discount += basePrice * 0.10m;
        else if (order.Customer.IsVip)
            discount += basePrice * 0.05m;

        if (order.DiscountCode != null && order.DiscountCode.StartsWith("SAVE"))
            discount += basePrice * 0.08m;
        else if (order.DiscountCode != null && order.DiscountCode.StartsWith("SHIP"))
            discount += shippingCost * 0.50m;

        if (order.Items.Count > 10 && basePrice > 1000m)
            discount += basePrice * 0.05m;

        return discount;
    }

    private static decimal DetermineTaxRate(ShipmentDestination destination)
    {
        if (destination.CountryCode == "US")
            return 0.08m;
        else if (destination.CountryCode == "CA")
            return 0.13m;
        else if (destination.IsInternational)
            return 0.20m;
        return 0m;
    }

    private static (decimal handlingFee, bool requiresSignature) CalculateHandlingFees(List<OrderItem> items, string carrier)
    {
        bool requiresSignature = false;
        decimal handlingFee = 0m;

        foreach (var item in items)
        {
            if (item.IsHazmat)
            {
                handlingFee += 15m;
                requiresSignature = true;
            }
            if (item.IsFragile && carrier != "FREIGHT")
                handlingFee += 5m;
            if (item.IsOversized)
                handlingFee += 20m;
        }

        return (handlingFee, requiresSignature);
    }

    private static bool DetermineRequiresApproval(ShipmentOrder order, decimal basePrice, decimal discount)
    {
        if ((basePrice - discount) > 10000m || order.Destination.IsRestrictedZone)
            return true;
        else if (order.Customer.RiskScore > 7 && (basePrice - discount) > 2000m)
            return true;
        return false;
    }
}
