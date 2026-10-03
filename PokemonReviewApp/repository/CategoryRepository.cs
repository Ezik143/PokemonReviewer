using PokemonReviewApp.Data;
using PokemonReviewApp.interfaces;
using PokemonReviewApp.Models.Entities;

namespace PokemonReviewApp.repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool CategoryExist(int id)
        {
            var category = _context.Categories.Any(c => c.Id == id);

            if (!category)
            {
                return false;
            }

            return true;
        }

        public ICollection<Category> GetCategories()
        {
            var categories = _context.Categories.ToList();

            return categories;
        }

        public Category? GetCategory(int id)
        {

            var category = _context.Categories.FirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                return null;
            }

            return category;
        }

        public ICollection<Pokemon> GetPokemonByCategory(int categoryId)
        {
            return _context.PokemonCategories
            .Where(e => e.CategoryId == categoryId)
            .Select(e => e.Pokemon)
            .ToList();
        }
    }
}
