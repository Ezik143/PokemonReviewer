using System;
using System.ComponentModel.DataAnnotations;

namespace PokemonReviewApp.Models.Dto;

public class ReviewCreateDto
{
    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MaxLength(500)]
    public string Text { get; set; } = string.Empty;

    [Range(1, 5)]
    public int Rating { get; set; }

    public int PokemonId { get; set; }

    public int ReviewerId { get; set; }
}