using PokemonReviewApp.Models.Dto;
using PokemonReviewApp.Models.Entities;
using Riok.Mapperly.Abstractions;

namespace PokemonReviewApp.Mappers;

[Mapper]
public partial class PokemonOwnerMapper
{
    [MapperIgnoreSource(nameof(PokemonOwner.Pokemon))]
    [MapperIgnoreSource(nameof(PokemonOwner.Owner))]
    public partial PokemonOwnerDto ToDto(PokemonOwner pokemonOwner);

    public partial List<PokemonOwnerDto> ToDtoList(IEnumerable<PokemonOwner> pokemonOwners);

    [MapperIgnoreTarget(nameof(PokemonOwner.Pokemon))]
    [MapperIgnoreTarget(nameof(PokemonOwner.Owner))]
    public partial PokemonOwner ToEntity(PokemonOwnerDto dto);
}
