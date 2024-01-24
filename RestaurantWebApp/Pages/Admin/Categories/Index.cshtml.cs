using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Restaurant.DataAccess.Data;
using Restaurant.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Repository.IRepository;

namespace AbbyWeb.Pages.Admin.Categories;

public class IndexModel : PageModel
{
	private readonly ICategoryRepository _dbCategory;
	public IEnumerable<Category> Categories { get; set; }
    public IndexModel(ICategoryRepository dbCategory)
    {
		_dbCategory = dbCategory;
    }

    public void OnGet()
    {
        Categories = _dbCategory.GetAll();
    }
}
