using System;
using PokemonReviewApp.Data;
using PokemonReviewApp.interfaces;
using PokemonReviewApp.Models.Entities;

namespace PokemonReviewApp.repository;

public class ReviewRepository : IReviewRepository
{
    private readonly ApplicationDbContext _context;
    public ReviewRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public Review GetReview(int reviewId)
    {
        return _context.Reviews.FirstOrDefault(r => r.Id == reviewId);
    }

    public ICollection<Review> GetReviewOfPokemon(int pokeId)
    {
        return _context.Reviews.Where(r => r.Pokemon.Id == pokeId).ToList();
    }

    public ICollection<Review> GetReviews()
    {
        return _context.Reviews.ToList();
    }

    public bool ReviewExist(int reviewId)
    {
        return _context.Reviews.Any(r => r.Id == reviewId);
    }
}
