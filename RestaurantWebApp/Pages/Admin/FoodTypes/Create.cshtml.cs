using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Data;
using Restaurant.DataAccess.Repository;
using Restaurant.DataAccess.Repository.IRepository;
using Restaurant.Models;
using Restaurant.Utility;

namespace RestaurantWebApp.Pages.Admin.FoodTypes;

[BindProperties]
[Authorize(Roles = SD.ManagerRole)]
public class CreateModel : PageModel
{
	private readonly IUnitOfWork _unitOfWork;

	public FoodType FoodType { get; set; }

    public CreateModel(IUnitOfWork unitOfWork)
    {
		_unitOfWork = unitOfWork;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.FoodType.Add(FoodType);
            _unitOfWork.Save();
            TempData["success"] = "Food Type created successfully";
            return RedirectToPage("Index");
        }
        return Page();
    }
}
