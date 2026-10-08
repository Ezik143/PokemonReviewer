using System;
using PokemonReviewApp.Data;
using PokemonReviewApp.interfaces;
using PokemonReviewApp.Models.Entities;

namespace PokemonReviewApp.repository;

public class ReviewerRepository : IReviewerRepository
{
    private readonly ApplicationDbContext _context;
    public ReviewerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public bool CreateReviewer(Reviewer review)
    {
        _context.Add(review);
        return Save();
    }

    public bool UpdateReviewer(Reviewer reviewer)
    {
        _context.Update(reviewer);
        return Save();
    }

    public Reviewer GetReviewer(int reviewerId)
    {
        return _context.Reviewers.FirstOrDefault(r => r.Id == reviewerId);
    }

    public ICollection<Review> GetReviews()
    {
        return _context.Reviews.ToList();
    }

    public ICollection<Review> GetReviewsByReviewer(int reviewerId)
    {
        var reviews = _context.Reviews.Where(r => r.ReviewerId == reviewerId).ToList();
        return reviews;
    }

    public bool ReviewerExist(int reviewerId)
    {
        return _context.Reviewers.Any(r => r.Id == reviewerId);
    }

    public bool Save()
    {
        var save = _context.SaveChanges();

        return save > 0 ? true : false;
    }
}
