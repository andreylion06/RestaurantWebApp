using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Data;
using Restaurant.DataAccess.Repository.IRepository;
using Restaurant.Models;

namespace RestaurantWebApp.Pages.Admin.Categories;

[BindProperties]
public class CreateModel : PageModel
{
	private readonly IUnitOfWork _unitOfWork;

	public Category Category { get; set; }

	public CreateModel(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (Category.Name == Category.DisplayOrder.ToString())
        {
            ModelState.AddModelError("Category.Name", "The DisplayOrder cannot exactly match the Name.");
        }
        if (ModelState.IsValid)
        {
            _unitOfWork.Category.Add(Category);
            _unitOfWork.Save();
            TempData["success"] = "Category created successfully";
            return RedirectToPage("Index");
        }
        return Page();
    }
}
