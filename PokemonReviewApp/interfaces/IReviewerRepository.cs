using System;
using PokemonReviewApp.Models.Entities;

namespace PokemonReviewApp.interfaces;

public interface IReviewerRepository
{
    ICollection<Review> GetReviews();
    Reviewer GetReviewer(int reviewerId);
    ICollection<Review> GetReviewsByReviewer(int reviewerId);
    bool ReviewerExist(int reviewerId);
    bool CreateReviewer(Reviewer review);
    bool Save();
}
