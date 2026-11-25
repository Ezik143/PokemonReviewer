namespace PokemonReviewApp.Models.Entities;

public class Owner
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Gym { get; set; } = string.Empty;
    public int CountryId { get; set; }
    public Country Country { get; set; } = null!;
    public ICollection<PokemonOwner> PokemonOwners { get; set; } = new List<PokemonOwner>();
}
