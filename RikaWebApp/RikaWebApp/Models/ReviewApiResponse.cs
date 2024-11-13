namespace RikaWebApp.Models;
public class ReviewApiResponse
{
    public int Status { get; set; }
    public string Message { get; set; } = string.Empty;
    public List<ReviewModel> Reviews { get; set; } = new List<ReviewModel>();
}