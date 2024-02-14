using Restaurant.DataAccess.Data;
using Restaurant.DataAccess.Repository.IRepository;
using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DataAccess.Repository
{
	public class CategoryRepository : Repository<Category>, ICategoryRepository
	{
		private readonly MyApplicationDbContext _db;

		public CategoryRepository(MyApplicationDbContext db) : base(db)
		{
			_db = db;
		}

		public void Save()
		{
			_db.SaveChanges();
		}

		public void Update(Category category)
		{
			var objFromDb = _db.Category.FirstOrDefault(u => u.Id == category.Id);
			objFromDb.Name = category.Name;
			objFromDb.DisplayOrder = category.DisplayOrder;
		}
	}
}
