using System;
using System.ComponentModel.DataAnnotations;

namespace PokemonReviewAPI.Models.Dto;

public class CountryCreateDto
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
}