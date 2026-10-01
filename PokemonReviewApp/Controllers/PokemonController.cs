using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonRepository _pokemonRepository;
        public PokemonController(IPokemonRepository pokemonRepository)
        {
            _pokemonRepository = pokemonRepository;
        }

        [HttpGet]
        public IActionResult GetPokemons()
        {
            var pokemons = _pokemonRepository.GetPokemons();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pokemons);
        }

        [HttpGet("{pokeId}/rating")]
        public IActionResult GetPokemonRatings(int pokeId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_pokemonRepository.GetPokemonRating(pokeId));
        }
    }
}
