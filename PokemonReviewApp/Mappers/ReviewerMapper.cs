using PokemonReviewApp.Models.Dto;
using PokemonReviewApp.Models.Entities;
using Riok.Mapperly.Abstractions;

namespace PokemonReviewApp.Mappers;

[Mapper]
public partial class ReviewerMapper
{
    [MapperIgnoreSource(nameof(Reviewer.Reviews))]
    public partial ReviewerDto ToDto(Reviewer reviewer);

    public partial List<ReviewerDto> ToDtoList(IEnumerable<Reviewer> reviewers);
}
