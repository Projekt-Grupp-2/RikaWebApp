namespace RikaWebApp.Models;

public class ProductCardModel
{
    public string Name { get; set; } = string.Empty;

    public string? ShortDescription { get; set; }

    public List<Image> Images { get; set; } = new List<Image>();

    public Price Prices { get; set; } = new Price();

    public string? FirstImageUrl => Images.Count > 0 ? Images[0].ImageUrl : null;
}

public class Price
{
    public decimal Price1 { get; set; }

    public decimal? Discount { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool IsActive => StartDate <= DateTime.Now && EndDate >= DateTime.Now;

    public decimal DiscountPrice => Discount.HasValue ? Price1 - (Price1 * Discount.Value) : Price1;
}

public class Image
{
    public string? ImageUrl { get; set; }
}
