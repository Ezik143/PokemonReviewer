using System;
using System.ComponentModel.DataAnnotations;

namespace PokemonReviewApp.Models.Dto;

public class CategoryCreateDto
{
    [Required]
    [MaxLength(50)]
    public required string Name { get; set; }
}