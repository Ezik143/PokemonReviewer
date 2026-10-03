using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.interfaces;
using PokemonReviewApp.Mappers;


namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly OwnerMapper _ownerMapper;
        private readonly PokemonMapper _pokemonMapper;
        private readonly IPokemonRepository _pokemonRepository;
        public OwnerController(IOwnerRepository ownerRepository, OwnerMapper ownerMapper, PokemonMapper pokemonMapper, IPokemonRepository pokemonRepository)
        {
            _ownerRepository = ownerRepository;
            _ownerMapper = ownerMapper;
            _pokemonMapper = pokemonMapper;
            _pokemonRepository = pokemonRepository;
        }

        [HttpGet]
        public IActionResult GetOwners()
        {
            var owners = _ownerRepository.GetOwners();
            var ownersDto = _ownerMapper.ToDtoList(owners);
            return Ok(ownersDto);
        }

        [HttpGet("{ownerId}/GetOwnerById")]
        public IActionResult GetOwnerById(int ownerId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (!_ownerRepository.OwnerExist(ownerId))
            {
                return NotFound("Owner not found");
            }
            var owner = _ownerRepository.GetOwnerById(ownerId);
            var ownerDto = _ownerMapper.ToDto(owner);
            return Ok(ownerDto);
        }

        [HttpGet("{pokeId}/GetOwnersofPokemon")]
        public IActionResult GetOwnersofPokemon(int pokeId)
        {
            if (!_pokemonRepository.PokemonExist(pokeId))
                return NotFound("Pokemon not found");
            var owners = _ownerRepository.GetOwnersofPokemon(pokeId);
            var ownersDto = _ownerMapper.ToDtoList(owners);
            return Ok(ownersDto);
        }

        [HttpGet("{ownerId}/GetPokemonByOwner")]
        public IActionResult GetPokemonByOwner(int ownerId)
        {
            if (!_ownerRepository.OwnerExist(ownerId))
            {
                return NotFound("Owner not found");
            }

            var pokemons = _ownerRepository.GetPokemonByOwner(ownerId);
            var pokemonsDto = _pokemonMapper.ToDtoList(pokemons);
            return Ok(pokemonsDto);
        }
    }
}
