using System;
using PokemonReviewApp.Data;
using PokemonReviewApp.interfaces;
using PokemonReviewApp.Models.Dto;
using PokemonReviewApp.Models.Entities;

namespace PokemonReviewApp.repository;

public class ReviewRepository : IReviewRepository
{
    private readonly ApplicationDbContext _context;
    public ReviewRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public bool CreateReviews(Review createReview)
    {
        _context.Add(createReview);
        return Save();
    }

    public bool UpdateReview(Review review)
    {
        _context.Update(review);
        return Save();
    }

    public bool DeleteReview(Review review)
    {
        _context.Remove(review);
        return Save();
    }

    public Review GetReview(int reviewId)
    {
        return _context.Reviews.FirstOrDefault(r => r.Id == reviewId);
    }

    public ICollection<Review> GetReviewOfPokemon(int pokeId)
    {
        return _context.Reviews.Where(r => r.PokemonId == pokeId).ToList();
    }

    public ICollection<Review> GetReviews()
    {
        return _context.Reviews.ToList();
    }

    public bool ReviewExist(int reviewId)
    {
        return _context.Reviews.Any(r => r.Id == reviewId);
    }

    public bool Save()
    {
        var save = _context.SaveChanges();
        return save > 0 ? true : false;
    }
}
