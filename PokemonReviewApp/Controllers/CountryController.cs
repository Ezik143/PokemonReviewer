using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.interfaces;
using PokemonReviewApp.Mappers;
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
        public IActionResult GetCountries()
        {
            var countries = _countryRepository.GetCountries();
            var countriesDto = _countryMapper.ToDtoList(countries);
            return Ok(countriesDto);
        }

        [HttpGet("{id}")]
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
    }
}
