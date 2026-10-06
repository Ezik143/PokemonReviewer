using System;
using PokemonReviewApp.Models.Dto;
using PokemonReviewApp.Models.Entities;
using Riok.Mapperly.Abstractions;

namespace PokemonReviewApp.Mappers;

[Mapper]
public partial class OwnerMapper
{
    [MapperIgnoreSource(nameof(Owner.Country))]
    [MapperIgnoreSource(nameof(Owner.PokemonOwners))]
    public partial OwnerDto ToDto(Owner owner);

    public partial List<OwnerDto> ToDtoList(IEnumerable<Owner> owners);

    [MapperIgnoreTarget(nameof(Owner.Country))]
    [MapperIgnoreTarget(nameof(Owner.PokemonOwners))]
    public partial Owner toEntity(OwnerDto dto);
}
