using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Data;
using Restaurant.DataAccess.Repository.IRepository;
using Restaurant.Models;
using Restaurant.Utility;

namespace RestaurantWebApp.Pages.Admin.Categories;

[BindProperties]
[Authorize(Roles = SD.ManagerRole)]
public class DeleteModel : PageModel
{
	private readonly IUnitOfWork _unitOfWork;

	public Category Category { get; set; }

	public DeleteModel(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public void OnGet(int id)
    {
        Category = _unitOfWork.Category.GetFirstOrDefault(cat => cat.Id == id);
    }

    public async Task<IActionResult> OnPost()
    {
            var categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(cat => cat.Id == Category.Id);
            if (categoryFromDb != null)
            {
			_unitOfWork.Category.Remove(categoryFromDb);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToPage("Index");

        }

        return Page();
    }
}
