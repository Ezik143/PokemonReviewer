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
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;
        private readonly CountryMapper _countryMapper;
        private readonly OwnerMapper _ownerMapper;
        public CountryController(ICountryRepository countryRepository, CountryMapper countryMapper, OwnerMapper ownerMapper)
        {
            _countryRepository = countryRepository;
            _countryMapper = countryMapper;
            _ownerMapper = ownerMapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CountryDto>))]
        public IActionResult GetCountries()
        {
            var countries = _countryRepository.GetCountries();
            var countriesDto = _countryMapper.ToDtoList(countries);
            return Ok(countriesDto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CountryDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCountry(int id)
        {
            if (!_countryRepository.CountryExist(id))
            {
                return NotFound("Country not found");
            }
            var country = _countryRepository.GetCountry(id)!;
            var countryDto = _countryMapper.ToDto(country);
            return Ok(countryDto);
        }

        [HttpGet("{OwnerId}/GetCountryByOwner")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CountryDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCountryByOwner(int OwnerId)
        {
            var country = _countryRepository.GetCountryByOwner(OwnerId);

            if (country == null)
                return NotFound("Country not found");

            if (!ModelState.IsValid)
                return BadRequest();

            var countryDto = _countryMapper.ToDto(country);
            return Ok(countryDto);
        }

        [HttpGet("{countryId}/GetOwnersFromCountry")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<OwnerDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetOwnersFromCountry(int countryId)
        {
            if (!_countryRepository.CountryExist(countryId))
            {
                return NotFound();
            }
            var Owners = _countryRepository.GetOwnersFromCountry(countryId);

            var ownersDto = _ownerMapper.ToDtoList(Owners);
            return Ok(ownersDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateCountry(CountryCreateDto createCountry)
        {
            var country = _countryRepository.GetCountries()
            .Where(c => c.Name.Trim().ToUpper() == createCountry.Name.Trim().ToUpper()).FirstOrDefault();

            if (country != null)
            {
                return BadRequest("country already exist");
            }

            var countryMap = _countryMapper.toEntitiy(createCountry);
            if (!_countryRepository.CreateCountry(countryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return BadRequest(ModelState);
            }
            return Ok("Successfully created");
        }
    }
}
