using APIpronia.Entities;
using Microsoft.EntityFrameworkCore;

namespace APIpronia.DAL
{
	public class AppDBContext :DbContext
	{
		public AppDBContext(DbContextOptions<AppDBContext>options ) :base(options) 
		{ 
		
		}
		public DbSet<Category>Categories { get; set; }
	}
}
