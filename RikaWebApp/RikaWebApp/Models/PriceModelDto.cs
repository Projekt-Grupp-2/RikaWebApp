namespace RikaWebApp.Models;

public class PriceModelDto
{
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }

        public decimal? Price { get; set; }

        public decimal? Discount { get; set; }

        public decimal? DiscountPrice { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool? IsActive { get; set; }
}
