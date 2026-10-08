using PokemonReviewApp.Models.Dto;
using PokemonReviewApp.Models.Entities;
using Riok.Mapperly.Abstractions;

namespace PokemonReviewApp.Mappers;

[Mapper]
public partial class PokemonMapper
{
    [MapperIgnoreSource(nameof(Pokemon.Reviews))]
    [MapperIgnoreSource(nameof(Pokemon.PokemonOwners))]
    [MapperIgnoreSource(nameof(Pokemon.PokemonCategories))]
    public partial PokemonDto ToDto(Pokemon pokemon);

    public partial List<PokemonDto> ToDtoList(
        IEnumerable<Pokemon> pokemons);

    [MapperIgnoreTarget(nameof(Pokemon.Id))]
    [MapperIgnoreTarget(nameof(Pokemon.Reviews))]
    [MapperIgnoreTarget(nameof(Pokemon.PokemonOwners))]
    [MapperIgnoreTarget(nameof(Pokemon.PokemonCategories))]
    public partial Pokemon ToEntity(PokemonCreateDto dto);
}