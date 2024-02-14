using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Restaurant.DataAccess.Data;
using Restaurant.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Restaurant.Utility;

namespace RestaurantWebApp.Pages.Admin.FoodTypes;

[Authorize(Roles = SD.ManagerRole)]
public class IndexModel : PageModel
{
	private readonly IUnitOfWork _unitOfWork;

	public IEnumerable<FoodType> FoodTypes { get; set; }

	public IndexModel(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

    public void OnGet()
    {
		FoodTypes = _unitOfWork.FoodType.GetAll();
    }
}
