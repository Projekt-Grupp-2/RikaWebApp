using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RikaWebApp.Models;

public class ProductModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string ShortDescription { get; set; } = null!;

    public string? LongDescription { get; set; }

    public Guid CategoryId { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsTopseller { get; set; }

    public virtual CategoryModel Category { get; set; } = new CategoryModel();

    public virtual ICollection<ImageModel> Images { get; set; } = new List<ImageModel>();

    public virtual ICollection<PriceModel> Prices { get; set; } = new List<PriceModel>();

    public virtual ICollection<ReviewModel>? Reviews { get; set; } = new List<ReviewModel>();

    public virtual ICollection<WarehouseModel>? Warehouses { get; set; } = new List<WarehouseModel>();
}

public class CategoryModel
{
    public string? Name { get; set; }

    public string? Icon { get; set; }
}

public class ColorModel
{
    public Guid Id { get; set; }
    public string? Name { get; set; }

    public string? HexadecimalColor { get; set; }

}

public class ImageModel
{
    public Guid? ProductId { get; set; }

    public string? ImageUrl { get; set; }

}

public class PriceModel
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }

    public decimal? Price1 { get; set; }

    public decimal? Discount { get; set; }

    public decimal? DiscountPrice { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool? IsActive { get; set; }

}

public class ReviewModel
{
    public Guid? ProductId { get; set; }

    public int? Stars { get; set; }

    public string? Text { get; set; }

}


public class ReviewUpdateRequest
{
    public Guid Id { get; set; }

    public Guid? ProductId { get; set; }

    public int? Stars { get; set; }

    public string? Text { get; set; }
}

public class SizeModel
{
    public string? Name { get; set; }


}

public class WarehouseModel
{
    public Guid? ProductId { get; set; }

    public Guid ColorId { get; set; }

    public Guid? SizeId { get; set; }

    public int? CurrentStock { get; set; }


}





