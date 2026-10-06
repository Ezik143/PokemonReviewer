using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.interfaces;
using PokemonReviewApp.Mappers;
using PokemonReviewApp.Models.Dto;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly CategoryMapper _categoryMapper;
        private readonly PokemonMapper _pokemonMapper;
        public CategoryController(ICategoryRepository categoryRepository, CategoryMapper categoryMapper, PokemonMapper pokemonMapper)
        {
            _categoryRepository = categoryRepository;
            _categoryMapper = categoryMapper;
            _pokemonMapper = pokemonMapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CategoryDto>))]
        public IActionResult GetCategories()
        {
            var categories = _categoryRepository.GetCategories();

            var categoriesDto = _categoryMapper.ToDtoList(categories);

            return Ok(categoriesDto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCategory(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_categoryRepository.CategoryExist(id))
            {
                return NotFound("Not Found");
            }

            var category = _categoryRepository.GetCategory(id)!;
            var categoryDto = _categoryMapper.categoryDto(category);
            return Ok(categoryDto);
        }

        [HttpGet("{categoryId}/PokemonCategory")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PokemonDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPokemonByCategory(int categoryId)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_categoryRepository.CategoryExist(categoryId))
            {
                return NotFound("Not found");
            }
            var pokemonByCategory = _categoryRepository.GetPokemonByCategory(categoryId);

            var pokemonDto = _pokemonMapper.ToDtoList(pokemonByCategory);
            return Ok(pokemonDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateCategory(CategoryDto categoryCreate)
        {
            if (categoryCreate == null)
                return BadRequest();

            var categories = _categoryRepository.GetCategories()
                .Where(c => c.Name.Trim().ToUpper() == categoryCreate.Name.Trim().ToUpper())
                .FirstOrDefault();

            if (categories != null)
            {
                return BadRequest("Pokemon already exist");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryMap = _categoryMapper.ToEntity(categoryCreate);

            if (!_categoryRepository.CreateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return BadRequest(ModelState);
            }

            return Ok("sucessfully created");
        }

    }
}
