using System;
using PokemonReviewApp.Models.Dto;
using PokemonReviewApp.Models.Entities;

namespace PokemonReviewApp.interfaces;

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
