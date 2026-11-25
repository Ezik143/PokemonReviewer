using System;
using PokemonReviewAPI.Models.Dto;
using PokemonReviewAPI.Models.Entities;

namespace PokemonReviewAPI.interfaces;

public interface IReviewRepository
{
    ICollection<Review> GetReviews();
    Review GetReview(int reviewId);
    ICollection<Review> GetReviewOfPokemon(int pokeId);
    bool ReviewExist(int reviewId);
    bool CreateReviews(Review createReview);
    bool UpdateReview(Review review);
    bool DeleteReview(Review review);
    bool Save();
}
