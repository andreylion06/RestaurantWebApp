using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Data;
using Restaurant.DataAccess.Repository.IRepository;
using Restaurant.Models;

namespace RestaurantWebApp.Pages.Admin.FoodTypes;

[BindProperties]
public class EditModel : PageModel
{
	private readonly IUnitOfWork _unitOfWork;

	public FoodType FoodType { get; set; }

	public EditModel(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public void OnGet(int id)
    {
        FoodType = _unitOfWork.FoodType.GetFirstOrDefault(u => u.Id == id);
    }

	public IActionResult OnPost()
	{
		if (ModelState.IsValid)
		{
			_unitOfWork.FoodType.Update(FoodType);
			_unitOfWork.Save();
			TempData["success"] = "Food Type updated successfully";
			return RedirectToPage("Index");
		}
		return Page();
	}
}
