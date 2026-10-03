using System;
using PokemonReviewApp.Models.Entities;

namespace PokemonReviewApp.interfaces;

public interface ICountryRepository
{
    ICollection<Country> GetCountries();
    Country? GetCountry(int id);
    Country? GetCountryByOwner(int OwnerId);
    ICollection<Owner> GetOwnersFromCountry(int countryId);
    bool CountryExist(int countryId);
}
