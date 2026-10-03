using System;
using PokemonReviewApp.Data;
using PokemonReviewApp.interfaces;
using PokemonReviewApp.Models.Entities;

namespace PokemonReviewApp.repository;

public class OwnerRepository : IOwnerRepository
{
    private readonly ApplicationDbContext _context;
    public OwnerRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public Owner GetOwnerById(int ownerId)
    {
        return _context.Owners.FirstOrDefault(o => o.Id == ownerId);
    }

    public ICollection<Owner> GetOwners()
    {
        return _context.Owners.ToList();
    }

    public ICollection<Owner> GetOwnersofPokemon(int pokeId)
    {
        var owners = _context.Owners.Where(o => o.PokemonOwners.Any(po => po.PokemonId == pokeId)).ToList();
        return owners;
    }

    public ICollection<Pokemon> GetPokemonByOwner(int ownerId)
    {
        var pokemons = _context.Pokemons.Where(p => p.PokemonOwners.Any(po => po.OwnerId == ownerId)).ToList();

        return pokemons;
    }

    public bool OwnerExist(int ownerId)
    {
        return _context.Owners.Any(o => o.Id == ownerId);
    }
}
