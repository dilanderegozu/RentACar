using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.EntityLayer.Entities;

namespace Rentaly.WebUI.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService _carService;
        private readonly ICategoryService _categoryService;
        private readonly IBranchService _branchService;

        public CarController(ICarService carService, ICategoryService categoryService, IBranchService branchService)
        {
            _carService = carService;
            _categoryService = categoryService;
            _branchService = branchService;
        }

        public async Task<IActionResult> CarList()
        {
            var values = await _carService.TGetListAsync();
            return View(values);
     }
        [HttpGet]
        public async Task<IActionResult> CreateCar()
        {
            ViewBag.Categories =new SelectList(await _categoryService.TGetListAsync(),"CategoryId","CategoryName");
            ViewBag.Branches = new SelectList(await _branchService.TGetListAsync(), "BranchId", "BranchName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCar(Car car)
        {
            await _carService.TInsertAsync(car);
            return RedirectToAction("CarList");
        }
    }
}

