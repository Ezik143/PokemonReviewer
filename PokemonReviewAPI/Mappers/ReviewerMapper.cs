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

    [MapperIgnoreTarget(nameof(Reviewer.Id))]
    [MapperIgnoreTarget(nameof(Reviewer.Reviews))]
    public partial Reviewer toEntity(ReviewerCreateDto dto);

    [MapperIgnoreTarget(nameof(Reviewer.Id))]
    [MapperIgnoreTarget(nameof(Reviewer.Reviews))]
    public partial void Update(ReviewerCreateDto dto, Reviewer reviewer);
}
