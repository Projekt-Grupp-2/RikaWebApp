using System.ComponentModel.DataAnnotations;

namespace RikaWebApp.Data;

public class UserAddress
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Address { get; set; } = null!;

    public string City { get; set; } = null!;

    public string PostalCode { get; set; } = null!;

}
