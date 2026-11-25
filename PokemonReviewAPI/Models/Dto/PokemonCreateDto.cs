using System;
using System.ComponentModel.DataAnnotations;

namespace PokemonReviewApp.Models.Dto;

public class PokemonCreateDto
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    public DateTime BirthDate { get; set; }
}