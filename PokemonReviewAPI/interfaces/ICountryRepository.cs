using System;
using PokemonReviewAPI.Models.Entities;

namespace PokemonReviewAPI.interfaces;

public interface ICountryRepository
{
    ICollection<Country> GetCountries();
    Country? GetCountry(int id);
    Country? GetCountryByOwner(int OwnerId);
    ICollection<Owner> GetOwnersFromCountry(int countryId);
    bool CountryExist(int countryId);
    bool CreateCountry(Country country);
    bool UpdateCountry(Country country);
    bool DeleteCountry(Country country);

    bool Save();
}
