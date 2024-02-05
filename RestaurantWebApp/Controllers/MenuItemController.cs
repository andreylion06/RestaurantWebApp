using Microsoft.AspNetCore.Mvc;
using Restaurant.DataAccess.Repository.IRepository;

namespace RestaurantWebApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MenuItemController : Controller
	{
		private readonly IUnitOfWork _unitOfOfWork;
		private readonly IWebHostEnvironment _hostEnvironment;

		public MenuItemController(IUnitOfWork unitOfOfWork, IWebHostEnvironment hostEnvironment)
		{
			_unitOfOfWork = unitOfOfWork;
			_hostEnvironment = hostEnvironment;
		}

		[HttpGet]
		public IActionResult Get()
		{
			var menuItemList = _unitOfOfWork.MenuItem.GetAll(includeProperties: "Category,FoodType");
			return Json(new { data = menuItemList });
		}

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
			var objFromDb = _unitOfOfWork.MenuItem.GetFirstOrDefault(x => x.Id == id);
            var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, objFromDb.Image.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfOfWork.MenuItem.Remove(objFromDb);
			_unitOfOfWork.Save();

			return Json(new { success  = true, message = "Delete successful." });
        }
    }
}
