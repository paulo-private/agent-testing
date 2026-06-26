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
        if (order == null)
            throw new ArgumentNullException(nameof(order));
        if (order.CustomerId.Length == 0)
            throw new ArgumentException("Order must have a customer.");
        if (order.Items.Count == 0)
            throw new ArgumentException("Order must contain at least one item.");
        if (order.Destination.CountryCode.Length == 0)
            throw new ArgumentException("Order must have a valid destination.");

        string carrier;
        int deliveryDays;

        switch (order.ShippingTier)
        {
            case "express":
                if (order.Weight > 30 || order.IsOversized)
                {
                    carrier = "FREIGHT";
                    deliveryDays = 2;
                }
                else if (order.Destination.IsInternational && order.Destination.CountryCode != "CA")
                {
                    carrier = "INTL_EXPRESS";
                    deliveryDays = 3;
                }
                else
                {
                    carrier = "LOCAL_EXPRESS";
                    deliveryDays = 1;
                }
                break;
            case "standard":
                if (order.Destination.IsInternational)
                {
                    carrier = "INTL_STANDARD";
                    deliveryDays = 14;
                }
                else if (order.Weight > 50)
                {
                    carrier = "FREIGHT";
                    deliveryDays = 5;
                }
                else
                {
                    carrier = "STANDARD";
                    deliveryDays = 5;
                }
                break;
            case "economy":
                carrier = order.Destination.IsInternational ? "INTL_ECONOMY" : "GROUND";
                deliveryDays = order.Destination.IsInternational ? 21 : 7;
                break;
            case "white_glove":
                if (order.Destination.IsInternational)
                {
                    carrier = "INTL_PREMIUM";
                    deliveryDays = 5;
                }
                else
                {
                    carrier = "WHITE_GLOVE";
                    deliveryDays = 2;
                }
                break;
            default:
                carrier = "STANDARD";
                deliveryDays = 5;
                break;
        }

        decimal basePrice = order.Items.Sum(i => i.Price * i.Quantity);

        decimal shippingCost;
        if (carrier == "FREIGHT")
            shippingCost = (decimal)order.Weight * 2.5m;
        else if (carrier == "INTL_EXPRESS" || carrier == "INTL_STANDARD")
            shippingCost = basePrice * 0.15m + 25m;
        else if (carrier == "INTL_ECONOMY")
            shippingCost = basePrice * 0.08m + 10m;
        else if (carrier == "INTL_PREMIUM")
            shippingCost = basePrice * 0.20m + 50m;
        else if (carrier == "WHITE_GLOVE")
            shippingCost = basePrice * 0.12m + 30m;
        else
            shippingCost = (decimal)order.Weight * 0.5m + 5m;

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

        decimal taxRate = 0m;
        if (order.Destination.CountryCode == "US")
            taxRate = 0.08m;
        else if (order.Destination.CountryCode == "CA")
            taxRate = 0.13m;
        else if (order.Destination.IsInternational)
            taxRate = 0.20m;

        decimal tax = (basePrice - discount + shippingCost) * taxRate;

        bool requiresSignature = false;
        decimal handlingFee = 0m;

        foreach (var item in order.Items)
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

        bool requiresApproval = false;
        if ((basePrice - discount) > 10000m || order.Destination.IsRestrictedZone)
            requiresApproval = true;
        else if (order.Customer.RiskScore > 7 && (basePrice - discount) > 2000m)
            requiresApproval = true;

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
}
