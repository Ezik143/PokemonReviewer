using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.interfaces;
using PokemonReviewApp.Mappers;
using PokemonReviewApp.Models.Dto;
using PokemonReviewApp.Models.Entities;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly ReviewMapper _reviewMapper;
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IReviewerRepository _reviewerRepository;
        public ReviewController(IReviewRepository reviewRepository, ReviewMapper reviewMapper, IPokemonRepository pokemonRepository, IReviewerRepository reviewerRepository)
        {
            _reviewRepository = reviewRepository;
            _reviewMapper = reviewMapper;
            _pokemonRepository = pokemonRepository;
            _reviewerRepository = reviewerRepository;
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

            var reviews = _reviewRepository.GetReviews();
            var reviewsDto = _reviewMapper.ToDtoList(reviews);
            return Ok(reviewsDto);
        }

        [HttpGet("{reviewId}/GetReview")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReviewDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetReview(int reviewId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_reviewRepository.ReviewExist(reviewId))
                return NotFound("Review not found");

            var review = _reviewRepository.GetReview(reviewId)!;
            var reviewDto = _reviewMapper.ToDto(review);
            return Ok(reviewDto);
        }

        [HttpGet("{pokeId}/GetReviewsByPokemon")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ReviewDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetReviewsByPokemon(int pokeId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_pokemonRepository.PokemonExist(pokeId))
                return NotFound("Pokemon not found");

            var reviews = _reviewRepository.GetReviewOfPokemon(pokeId);
            var reviewsDto = _reviewMapper.ToDtoList(reviews);
            return Ok(reviewsDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult createReview(ReviewCreateDto reviewDto)
        {
            if (reviewDto == null)
                return BadRequest(ModelState);

            if (!_pokemonRepository.PokemonExist(reviewDto.PokemonId))
                return NotFound("Pokemon not found");

            if (!_reviewerRepository.ReviewerExist(reviewDto.ReviewerId))
                return NotFound("Reviewer not found");

            var reviewMap = _reviewMapper.toEntity(reviewDto);
            reviewMap.PokemonId = reviewDto.PokemonId;
            reviewMap.ReviewerId = reviewDto.ReviewerId;
            if (!_reviewRepository.CreateReviews(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return BadRequest(ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{reviewId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateReview(int reviewId, ReviewCreateDto reviewUpdate)
        {
            if (reviewUpdate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_reviewRepository.ReviewExist(reviewId))
                return NotFound("Review not found");

            if (!_pokemonRepository.PokemonExist(reviewUpdate.PokemonId))
                return NotFound("Pokemon not found");

            if (!_reviewerRepository.ReviewerExist(reviewUpdate.ReviewerId))
                return NotFound("Reviewer not found");

            var review = _reviewRepository.GetReview(reviewId);

            _reviewMapper.Update(reviewUpdate, review);
            review.PokemonId = reviewUpdate.PokemonId;
            review.ReviewerId = reviewUpdate.ReviewerId;

            if (!_reviewRepository.UpdateReview(review))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return BadRequest(ModelState);
            }

            return Ok("Successfully updated");
        }

        [HttpDelete("{reviewId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteReview(int reviewId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_reviewRepository.ReviewExist(reviewId))
                return NotFound("Review not found");

            var review = _reviewRepository.GetReview(reviewId);

            if (!_reviewRepository.DeleteReview(review))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return BadRequest(ModelState);
            }

            return Ok("Successfully deleted");
        }
    }
}
