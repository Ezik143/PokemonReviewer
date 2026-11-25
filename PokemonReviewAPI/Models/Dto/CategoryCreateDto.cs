using System;
using System.ComponentModel.DataAnnotations;

namespace PokemonReviewAPI.Models.Dto;

public class CategoryCreateDto
{
    [Required]
    [MaxLength(50)]
    public required string Name { get; set; }
}