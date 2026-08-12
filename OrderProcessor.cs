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
    private const string FreightCarrier = "FREIGHT";

    public ShipmentResult ProcessShipmentOrder(ShipmentOrder order)
    {
        ValidateOrder(order);

        var shippingOption = GetShippingOption(order);
        decimal basePrice = CalculateBasePrice(order);
        decimal shippingCost = CalculateShippingCost(order, shippingOption.Carrier, basePrice);
        decimal discount = CalculateDiscount(order, basePrice, shippingCost);
        decimal tax = CalculateTax(order, basePrice, discount, shippingCost);
        var handling = CalculateHandling(order.Items, shippingOption.Carrier);
        bool requiresApproval = RequiresApproval(order, basePrice, discount);
        decimal totalPrice = basePrice + shippingCost + handling.Fee + tax - discount;

        return new ShipmentResult
        {
            Carrier = shippingOption.Carrier,
            DeliveryDays = shippingOption.DeliveryDays,
            BasePrice = basePrice,
            ShippingCost = shippingCost,
            HandlingFee = handling.Fee,
            Discount = discount,
            Tax = tax,
            TotalPrice = totalPrice,
            RequiresSignature = handling.RequiresSignature,
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

    private static (string Carrier, int DeliveryDays) GetShippingOption(ShipmentOrder order)
    {
        return order.ShippingTier switch
        {
            "express" => GetExpressShippingOption(order),
            "standard" => GetStandardShippingOption(order),
            "economy" => GetEconomyShippingOption(order),
            "white_glove" => GetWhiteGloveShippingOption(order),
            _ => ("STANDARD", 5),
        };
    }

    private static (string Carrier, int DeliveryDays) GetExpressShippingOption(ShipmentOrder order)
    {
        if (order.Weight > 30 || order.IsOversized)
            return (FreightCarrier, 2);
        if (order.Destination.IsInternational && order.Destination.CountryCode != "CA")
            return ("INTL_EXPRESS", 3);

        return ("LOCAL_EXPRESS", 1);
    }

    private static (string Carrier, int DeliveryDays) GetStandardShippingOption(ShipmentOrder order)
    {
        if (order.Destination.IsInternational)
            return ("INTL_STANDARD", 14);
        if (order.Weight > 50)
            return (FreightCarrier, 5);

        return ("STANDARD", 5);
    }

    private static (string Carrier, int DeliveryDays) GetEconomyShippingOption(ShipmentOrder order)
    {
        return order.Destination.IsInternational ? ("INTL_ECONOMY", 21) : ("GROUND", 7);
    }

    private static (string Carrier, int DeliveryDays) GetWhiteGloveShippingOption(ShipmentOrder order)
    {
        if (order.Destination.IsInternational)
            return ("INTL_PREMIUM", 5);

        return ("WHITE_GLOVE", 2);
    }

    private static decimal CalculateBasePrice(ShipmentOrder order)
    {
        return order.Items.Sum(i => i.Price * i.Quantity);
    }

    private static decimal CalculateShippingCost(ShipmentOrder order, string carrier, decimal basePrice)
    {
        if (carrier == FreightCarrier)
            return (decimal)order.Weight * 2.5m;
        if (carrier == "INTL_EXPRESS" || carrier == "INTL_STANDARD")
            return basePrice * 0.15m + 25m;
        if (carrier == "INTL_ECONOMY")
            return basePrice * 0.08m + 10m;
        if (carrier == "INTL_PREMIUM")
            return basePrice * 0.20m + 50m;
        if (carrier == "WHITE_GLOVE")
            return basePrice * 0.12m + 30m;

        return (decimal)order.Weight * 0.5m + 5m;
    }

    private static decimal CalculateDiscount(ShipmentOrder order, decimal basePrice, decimal shippingCost)
    {
        return CalculateVipDiscount(order.Customer, basePrice)
            + CalculateDiscountCodeDiscount(order.DiscountCode, basePrice, shippingCost)
            + CalculateBulkDiscount(order, basePrice);
    }

    private static decimal CalculateVipDiscount(CustomerInfo customer, decimal basePrice)
    {
        if (customer.IsVip && basePrice > 500m)
            return basePrice * 0.10m;
        if (customer.IsVip)
            return basePrice * 0.05m;

        return 0m;
    }

    private static decimal CalculateDiscountCodeDiscount(string? discountCode, decimal basePrice, decimal shippingCost)
    {
        if (discountCode != null && discountCode.StartsWith("SAVE"))
            return basePrice * 0.08m;
        if (discountCode != null && discountCode.StartsWith("SHIP"))
            return shippingCost * 0.50m;

        return 0m;
    }

    private static decimal CalculateBulkDiscount(ShipmentOrder order, decimal basePrice)
    {
        if (order.Items.Count > 10 && basePrice > 1000m)
            return basePrice * 0.05m;

        return 0m;
    }

    private static decimal CalculateTax(ShipmentOrder order, decimal basePrice, decimal discount, decimal shippingCost)
    {
        decimal taxRate = GetTaxRate(order.Destination);
        return (basePrice - discount + shippingCost) * taxRate;
    }

    private static decimal GetTaxRate(ShipmentDestination destination)
    {
        if (destination.CountryCode == "US")
            return 0.08m;
        if (destination.CountryCode == "CA")
            return 0.13m;
        if (destination.IsInternational)
            return 0.20m;

        return 0m;
    }

    private static (decimal Fee, bool RequiresSignature) CalculateHandling(List<OrderItem> items, string carrier)
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
            if (item.IsFragile && carrier != FreightCarrier)
                handlingFee += 5m;
            if (item.IsOversized)
                handlingFee += 20m;
        }

        return (handlingFee, requiresSignature);
    }

    private static bool RequiresApproval(ShipmentOrder order, decimal basePrice, decimal discount)
    {
        decimal discountedPrice = basePrice - discount;

        return discountedPrice > 10000m
            || order.Destination.IsRestrictedZone
            || order.Customer.RiskScore > 7 && discountedPrice > 2000m;
    }
}
