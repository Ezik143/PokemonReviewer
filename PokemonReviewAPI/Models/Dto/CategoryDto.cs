using System;

namespace PokemonReviewAPI.Models.Dto;

public class CategoryDto
{
    public int Id { get; set; }

    public required string Name { get; set; }
}
