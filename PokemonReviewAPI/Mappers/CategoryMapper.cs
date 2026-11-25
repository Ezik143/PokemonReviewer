using System;
using PokemonReviewApp.Models.Dto;
using PokemonReviewApp.Models.Entities;
using Riok.Mapperly.Abstractions;

namespace PokemonReviewApp.Mappers;

[Mapper]
public partial class CategoryMapper
{
    [MapperIgnoreSource(nameof(category.PokemonCategories))]
    public partial CategoryDto categoryDto(Category category);

    public partial List<CategoryDto> ToDtoList(
   IEnumerable<Category> category);


    [MapperIgnoreTarget(nameof(Category.Id))]
    [MapperIgnoreTarget(nameof(Category.PokemonCategories))]
    public partial Category ToEntity(CategoryCreateDto dto);

    [MapperIgnoreTarget(nameof(Category.Id))]
    [MapperIgnoreTarget(nameof(Category.PokemonCategories))]
    public partial void Update(CategoryCreateDto dto, Category category);


}
