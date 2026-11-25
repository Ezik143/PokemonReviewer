using System;
using System.ComponentModel.DataAnnotations;

namespace PokemonReviewApp.Models.Dto;

public class OwnerCreateDto
{
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Gym { get; set; } = string.Empty;

    public int CountryId { get; set; }
}