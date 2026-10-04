using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.interfaces;
using PokemonReviewApp.Mappers;
using PokemonReviewApp.Models.Dto;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly ReviewMapper _reviewMapper;
        private readonly IPokemonRepository _pokemonRepository;
        public ReviewController(IReviewRepository reviewRepository, ReviewMapper reviewMapper, IPokemonRepository pokemonRepository)
        {
            _reviewRepository = reviewRepository;
            _reviewMapper = reviewMapper;
            _pokemonRepository = pokemonRepository;
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
    }
}
