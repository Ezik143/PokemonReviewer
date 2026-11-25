using System;
using PokemonReviewAPI.Models.Entities;

namespace PokemonReviewAPI.interfaces;

public interface IReviewerRepository
{
    ICollection<Review> GetReviews();
    Reviewer GetReviewer(int reviewerId);
    ICollection<Review> GetReviewsByReviewer(int reviewerId);
    bool ReviewerExist(int reviewerId);
    bool CreateReviewer(Reviewer review);
    bool UpdateReviewer(Reviewer reviewer);
    bool DeleteReviewer(Reviewer reviewer);
    bool Save();
}
