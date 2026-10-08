using System;
using PokemonReviewApp.Models.Entities;

namespace PokemonReviewApp.interfaces;

public interface IOwnerRepository
{
    ICollection<Owner> GetOwners();
    Owner GetOwnerById(int ownerId);
    ICollection<Owner> GetOwnersofPokemon(int pokeId);
    ICollection<Pokemon> GetPokemonByOwner(int ownerId);
    bool OwnerExist(int ownerId);
    bool CreateOwner(Owner owner);
    bool UpdateOwner(Owner owner);
    bool Save();
}
