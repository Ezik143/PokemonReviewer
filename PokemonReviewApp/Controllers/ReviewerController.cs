using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.interfaces;
using PokemonReviewApp.Mappers;
using PokemonReviewApp.Models.Dto;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewerController : ControllerBase
    {
        private readonly IReviewerRepository _reviewerRepository;
        private readonly ReviewerMapper _reviewerMapper;
        private readonly ReviewMapper _reviewMapper;
        public ReviewerController(IReviewerRepository reviewerRepository, ReviewerMapper reviewerMapper, ReviewMapper reviewMapper)
        {
            _reviewerRepository = reviewerRepository;
            _reviewerMapper = reviewerMapper;
            _reviewMapper = reviewMapper;
        }

        [HttpGet]
        public IActionResult GetReviews()
        {
            var reviews = _reviewerRepository.GetReviews();
            var reviewsDto = _reviewMapper.ToDtoList(reviews);
            return Ok(reviewsDto);
        }

        [HttpGet("{reviewerId}/GetReviewer")]
        public IActionResult GetReviewer(int reviewerId)
        {
            if (!_reviewerRepository.ReviewerExist(reviewerId))
                return NotFound();


            var reviewer = _reviewerRepository.GetReviewer(reviewerId);
            var reviewerDto = _reviewerMapper.ToDto(reviewer);
            return Ok(reviewerDto);
        }

        [HttpGet("{reviewerId}/GetReviewsByReviewer")]
        public IActionResult GetReviewsByReviewer(int reviewerId)
        {

            if (!_reviewerRepository.ReviewerExist(reviewerId))
                return NotFound();
            var review = _reviewerRepository.GetReviewsByReviewer(reviewerId);
            var reviewDto = _reviewMapper.ToDtoList(review);

            return Ok(reviewDto);
        }


    }
}
