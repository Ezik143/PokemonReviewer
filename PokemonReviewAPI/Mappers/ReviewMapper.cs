using PokemonReviewAPI.Models.Dto;
using PokemonReviewAPI.Models.Entities;
using Riok.Mapperly.Abstractions;

namespace PokemonReviewAPI.Mappers;

[Mapper]
public partial class ReviewMapper
{
    [MapperIgnoreSource(nameof(Review.Reviewer))]
    [MapperIgnoreSource(nameof(Review.Pokemon))]
    public partial ReviewDto ToDto(Review review);

    public partial List<ReviewDto> ToDtoList(IEnumerable<Review> reviews);

    [MapperIgnoreTarget(nameof(Review.Id))]
    [MapperIgnoreTarget(nameof(Review.Reviewer))]
    [MapperIgnoreTarget(nameof(Review.Pokemon))]
    public partial Review toEntity(ReviewCreateDto dto);

    [MapperIgnoreTarget(nameof(Review.Id))]
    [MapperIgnoreTarget(nameof(Review.Reviewer))]
    [MapperIgnoreTarget(nameof(Review.Pokemon))]
    public partial void Update(ReviewCreateDto dto, Review review);
}
