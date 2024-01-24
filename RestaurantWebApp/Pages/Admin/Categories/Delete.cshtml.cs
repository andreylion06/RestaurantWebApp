using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Data;
using Restaurant.DataAccess.Repository.IRepository;
using Restaurant.Models;

namespace AbbyWeb.Pages.Admin.Categories;

[BindProperties]
public class DeleteModel : PageModel
{
	private readonly ICategoryRepository _dbCategory;

	public Category Category { get; set; }

    public DeleteModel(ICategoryRepository dbCategory)
    {
		_dbCategory = dbCategory;
    }
    public void OnGet(int id)
    {
        Category = _dbCategory.GetFirstOrDefault(cat => cat.Id == id);
    }

    public async Task<IActionResult> OnPost()
    {
            var categoryFromDb = _dbCategory.GetFirstOrDefault(cat => cat.Id == Category.Id);
            if (categoryFromDb != null)
            {
			_dbCategory.Remove(categoryFromDb);
            _dbCategory.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToPage("Index");

        }

        return Page();
    }
}
