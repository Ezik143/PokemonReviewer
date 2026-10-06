using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.interfaces;
using PokemonReviewApp.Mappers;
using PokemonReviewApp.Models.Dto;


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
        private readonly ICountryRepository _countryRepository;
        public OwnerController(IOwnerRepository ownerRepository, OwnerMapper ownerMapper, PokemonMapper pokemonMapper, IPokemonRepository pokemonRepository, ICountryRepository countryRepository)
        {
            _ownerRepository = ownerRepository;
            _ownerMapper = ownerMapper;
            _pokemonMapper = pokemonMapper;
            _pokemonRepository = pokemonRepository;
            _countryRepository = countryRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<OwnerDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetOwners()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var owners = _ownerRepository.GetOwners();
            var ownersDto = _ownerMapper.ToDtoList(owners);
            return Ok(ownersDto);
        }

        [HttpGet("{ownerId}/GetOwnerById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OwnerDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetOwnerById(int ownerId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<OwnerDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetOwnersofPokemon(int pokeId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_pokemonRepository.PokemonExist(pokeId))
                return NotFound("Pokemon not found");
            var owners = _ownerRepository.GetOwnersofPokemon(pokeId);
            var ownersDto = _ownerMapper.ToDtoList(owners);
            return Ok(ownersDto);
        }

        [HttpGet("{ownerId}/GetPokemonByOwner")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PokemonDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPokemonByOwner(int ownerId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_ownerRepository.OwnerExist(ownerId))
            {
                return NotFound("Owner not found");
            }

            var pokemons = _ownerRepository.GetPokemonByOwner(ownerId);
            var pokemonsDto = _pokemonMapper.ToDtoList(pokemons);
            return Ok(pokemonsDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateOwner(OwnerDto ownerDto)
        {
            if (ownerDto == null) return BadRequest(ModelState);
            if (!_countryRepository.CountryExist(ownerDto.CountryId)) return NotFound("Country not found");
            var ownerMap = _ownerMapper.toEntity(ownerDto);
            ownerMap.CountryId = ownerDto.CountryId;
            if (!_ownerRepository.CreateOwner(ownerMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return BadRequest(ModelState);
            }
            return Ok("Successfully created");
        }
    }
}
