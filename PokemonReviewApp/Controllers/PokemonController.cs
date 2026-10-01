using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.interfaces;
using PokemonReviewApp.Mappers;
using PokemonReviewApp.Models.Entities;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly PokemonMapper _pokemonMapper;
        public PokemonController(IPokemonRepository pokemonRepository, PokemonMapper pokemonMapper)
        {
            _pokemonRepository = pokemonRepository;
            _pokemonMapper = pokemonMapper;
        }

        [HttpGet("pokemons")]
        public IActionResult GetPokemons()
        {
            var pokemons = _pokemonRepository.GetPokemons();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var pokemonDtos = _pokemonMapper.ToDtoList(pokemons);

            return Ok(pokemonDtos);
        }

        [HttpGet("{name}/name")]
        public IActionResult GetPokemonByName(string name)
        {
            var pokemon = _pokemonRepository.GetPokemonByName(name);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var pokemondto = _pokemonMapper.ToDto(pokemon);

            return Ok(pokemondto);
        }


        [HttpGet("{pokeId}")]
        public IActionResult GetPokemon(int pokeId)
        {
            if (!_pokemonRepository.PokemonExist(pokeId))
                return NotFound();

            var pokemon = _pokemonRepository.GetPokemonById(pokeId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pokemonDto = _pokemonMapper.ToDto(pokemon);
            return Ok(pokemonDto);
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
