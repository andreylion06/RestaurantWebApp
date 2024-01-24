using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Data;
using Restaurant.Models;

namespace AbbyWeb.Pages.Admin.FoodTypes;

[BindProperties]
public class EditModel : PageModel
{
    private readonly ApplicationDbContext _db;
    
    public FoodType FoodType { get; set; }

    public EditModel(ApplicationDbContext db)
    {
        _db = db;
    }
    public void OnGet(int id)
    {
        FoodType = _db.FoodType.Find(id);
    }

    public async Task<IActionResult> OnPost()
    {
        if (ModelState.IsValid)
        {
            _db.FoodType.Update(FoodType);
            await _db.SaveChangesAsync();
            TempData["success"] = "Food Type updated successfully";
            return RedirectToPage("Index");
        }
        return Page();
    }
}
