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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ReviewDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetReviews()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reviews = _reviewerRepository.GetReviews();
            var reviewsDto = _reviewMapper.ToDtoList(reviews);
            return Ok(reviewsDto);
        }

        [HttpGet("{reviewerId}/GetReviewer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReviewerDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetReviewer(int reviewerId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_reviewerRepository.ReviewerExist(reviewerId))
                return NotFound("Reviewer not found");

            var reviewer = _reviewerRepository.GetReviewer(reviewerId);
            var reviewerDto = _reviewerMapper.ToDto(reviewer);
            return Ok(reviewerDto);
        }

        [HttpGet("{reviewerId}/GetReviewsByReviewer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ReviewDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetReviewsByReviewer(int reviewerId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_reviewerRepository.ReviewerExist(reviewerId))
                return NotFound("Reviewer not found");
            var review = _reviewerRepository.GetReviewsByReviewer(reviewerId);
            var reviewDto = _reviewMapper.ToDtoList(review);

            return Ok(reviewDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateReview(ReviewerCreateDto reviewerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var reviewerMapper = _reviewerMapper.toEntity(reviewerDto);
            if (!_reviewerRepository.CreateReviewer(reviewerMapper))
            {
                ModelState.AddModelError("", "something wrong while saving");
                return BadRequest(ModelState);
            }
            return Ok("Successfully created");
        }

        [HttpPut("{reviewerId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateReviewer(int reviewerId, ReviewerCreateDto reviewerUpdate)
        {
            if (reviewerUpdate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_reviewerRepository.ReviewerExist(reviewerId))
                return NotFound("Reviewer not found");

            var reviewer = _reviewerRepository.GetReviewer(reviewerId);

            _reviewerMapper.Update(reviewerUpdate, reviewer);

            if (!_reviewerRepository.UpdateReviewer(reviewer))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return BadRequest(ModelState);
            }

            return Ok("Successfully updated");
        }
    }
}
