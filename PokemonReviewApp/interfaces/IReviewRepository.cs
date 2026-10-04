using System;
using PokemonReviewApp.Models.Entities;

namespace PokemonReviewApp.interfaces;

public interface IReviewRepository
{
    ICollection<Review> GetReviews();
    Review GetReview(int reviewId);
    ICollection<Review> GetReviewOfPokemon(int pokeId);
    bool ReviewExist(int reviewId);

}
