using System;
using PokemonReviewAPI.Models.Dto;
using PokemonReviewAPI.Models.Entities;
using Riok.Mapperly.Abstractions;

namespace PokemonReviewAPI.Mappers;

[Mapper]
public partial class CountryMapper
{

    [MapperIgnoreSource(nameof(Country.Owners))]
    public partial CountryDto ToDto(Country country);

    public partial List<CountryDto> ToDtoList(IEnumerable<Country> country);


    [MapperIgnoreTarget(nameof(Country.Id))]
    [MapperIgnoreTarget(nameof(Country.Owners))]
    public partial Country toEntitiy(CountryCreateDto dto);

    [MapperIgnoreTarget(nameof(Country.Id))]
    [MapperIgnoreTarget(nameof(Country.Owners))]
    public partial void Update(CountryCreateDto dto, Country country);
}
