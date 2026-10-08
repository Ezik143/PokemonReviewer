using System;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.interfaces;
using PokemonReviewApp.Models.Entities;

namespace PokemonReviewApp.repository;

public class CountryRepository : ICountryRepository
{
    private readonly ApplicationDbContext _context;
    public CountryRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public bool CountryExist(int countryId)
    {
        return _context.Countries.Any(c => c.Id == countryId);
    }

    public bool CreateCountry(Country country)
    {
        _context.Add(country);
        return Save();
    }

    public bool UpdateCountry(Country country)
    {
        _context.Update(country);
        return Save();
    }

    public ICollection<Country> GetCountries()
    {
        var countries = _context.Countries.ToList();
        return countries;
    }

    public Country? GetCountry(int id)
    {
        var country = _context.Countries.FirstOrDefault(c => c.Id == id);
        if (country == null)
        {
            return null;
        }
        return country;
    }

    public Country? GetCountryByOwner(int OwnerId)
    {
        var ownerCountry = _context.Countries
            .FirstOrDefault(c => c.Owners.Any(o => o.Id == OwnerId));

        if (ownerCountry == null)
        {
            return null;
        }
        return ownerCountry;
    }

    public ICollection<Owner> GetOwnersFromCountry(int countryId)
    {
        var ownersFromCountry = _context.Owners
        .Where(o => o.CountryId == countryId)
        .ToList();

        return ownersFromCountry;
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }


}
