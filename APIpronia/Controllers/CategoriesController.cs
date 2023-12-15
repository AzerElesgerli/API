
using APIpronia.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIpronia.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriesController : ControllerBase
	{
		private readonly IRepository _repository;
	//	private readonly DbSet<Category> _categories;
		public CategoriesController(IRepository repository)
		{ 
		_repository = repository;
			
		}
		[HttpGet]
		public async Task<IActionResult> Get(int page=1,int take=3)
		{
			

				return Ok(await _repository.GetAllAsync(x => x.Id > 2));
		
		}

		[HttpGet]
		[Route("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);

			Category existed = await _repository.GetByIdAsync(id);

			if (existed is null)
			{
				return StatusCode(StatusCodes.Status404NotFound);
			}

			return StatusCode(StatusCodes.Status200OK, existed);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromForm]CreateCategoryDto categoryDto)
		{

				Category category = new Category
				{
					Name = categoryDto.name,
				};
				await _repository.AddAsync(category);
				await _repository.SaveChangesAsync();
				return StatusCode(StatusCodes.Status201Created, category);
			}
		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id,string name)
		{
			if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);

			Category existed = await _repository.GetByIdAsync(id);

			if (existed is null)
			{
				return StatusCode(StatusCodes.Status404NotFound);
			}

			existed.Name = name;
			_repository.Update(existed);
			await _repository.SaveChangesAsync();
			return Accepted(existed);
		}
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);

			Category existed = await _repository.GetByIdAsync(id);

			if (existed is null)
			{
				return StatusCode(StatusCodes.Status404NotFound);
			}
			_repository.Delete(existed);
			await _repository.SaveChangesAsync();
			return NoContent();

		}


	}
}
