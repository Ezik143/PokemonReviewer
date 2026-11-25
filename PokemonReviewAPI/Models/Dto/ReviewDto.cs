namespace PokemonReviewApp.Models.Dto;

public class ReviewDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public int Rating { get; set; }
    public int PokemonId { get; set; }
    public int ReviewerId { get; set; }
}
