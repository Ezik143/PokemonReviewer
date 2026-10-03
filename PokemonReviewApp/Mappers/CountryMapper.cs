using System;
using PokemonReviewApp.Models.Dto;
using PokemonReviewApp.Models.Entities;
using Riok.Mapperly.Abstractions;

namespace PokemonReviewApp.Mappers;

[Mapper]
public partial class CountryMapper
{

    [MapperIgnoreSource(nameof(Country.Owners))]
    public partial CountryDto ToDto(Country country);

    public partial List<CountryDto> ToDtoList(IEnumerable<Country> country);
}
