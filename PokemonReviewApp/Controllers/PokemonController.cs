using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.interfaces;
using PokemonReviewApp.Mappers;
using PokemonReviewApp.Models.Dto;
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PokemonDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetPokemons()
        {
            var pokemons = _pokemonRepository.GetPokemons();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var pokemonDtos = _pokemonMapper.ToDtoList(pokemons);

            return Ok(pokemonDtos);
        }

        [HttpGet("{name}/name")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PokemonDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPokemonByName(string name)
        {
            var pokemon = _pokemonRepository.GetPokemonByName(name);

            if (pokemon == null)
                return NotFound("Pokemon not found");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var pokemondto = _pokemonMapper.ToDto(pokemon);

            return Ok(pokemondto);
        }


        [HttpGet("{pokeId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PokemonDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPokemon(int pokeId)
        {
            if (!_pokemonRepository.PokemonExist(pokeId))
                return NotFound();

            var pokemon = _pokemonRepository.GetPokemonById(pokeId)!;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pokemonDto = _pokemonMapper.ToDto(pokemon);
            return Ok(pokemonDto);
        }

        [HttpGet("{pokeId}/rating")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(decimal))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetPokemonRatings(int pokeId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_pokemonRepository.GetPokemonRating(pokeId));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreatePokemon(int catId, PokemonCreateDto pokemonCreate)
        {
            if (pokemonCreate == null)
                return BadRequest(ModelState);

            var pokemon = _pokemonRepository.GetPokemons().Where(p => p.Name.Trim().ToUpper() == pokemonCreate.Name.Trim().ToUpper()).FirstOrDefault();

            if (pokemon != null)
            {
                return BadRequest("Pokemon already exist");
            }

            var pokemonMap = _pokemonMapper.ToEntity(pokemonCreate);

            if (!_pokemonRepository.CreatePokemon(catId, pokemonMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return BadRequest(ModelState);
            }

            return Ok("sucessfully created");
        }

        [HttpPut("{pokeId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdatePokemon(int pokeId, PokemonCreateDto pokemonUpdate)
        {
            if (pokemonUpdate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_pokemonRepository.PokemonExist(pokeId))
                return NotFound("Pokemon not found");

            var pokemon = _pokemonRepository.GetPokemonById(pokeId)!;

            var duplicate = _pokemonRepository.GetPokemons()
                .Where(p => p.Id != pokeId && p.Name.Trim().ToUpper() == pokemonUpdate.Name.Trim().ToUpper())
                .FirstOrDefault();

            if (duplicate != null)
                return BadRequest("Pokemon already exist");

            _pokemonMapper.Update(pokemonUpdate, pokemon);

            if (!_pokemonRepository.UpdatePokemon(pokemon))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return BadRequest(ModelState);
            }

            return Ok("Successfully updated");
        }

        [HttpDelete("{pokeId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeletePokemon(int pokeId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_pokemonRepository.PokemonExist(pokeId))
                return NotFound("Pokemon not found");

            var pokemon = _pokemonRepository.GetPokemonById(pokeId)!;

            if (!_pokemonRepository.DeletePokemon(pokemon))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return BadRequest(ModelState);
            }

            return Ok("Successfully deleted");
        }
    }
}
