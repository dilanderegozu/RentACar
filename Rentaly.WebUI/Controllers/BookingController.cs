using Microsoft.AspNetCore.Mvc;
using Rentaly.Businesslayer.Abstract;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.EntityLayer.Entities;
using Rentaly.WebUI.Models;

namespace Rentaly.WebUI.Controllers
{
    public class BookingController : Controller
    {
        private readonly ICarService _carService;
        private readonly IBranchService _branchService;
        private readonly IVehicleTypeService _vehicleTypeService;
        private readonly IBookingService _bookingService;

        public BookingController(ICarService carService, IBranchService branchService, IVehicleTypeService vehicleTypeService, IBookingService bookingService)
        {
            _carService = carService;
            _branchService = branchService;
            _vehicleTypeService = vehicleTypeService;
            _bookingService = bookingService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id )
        {
            var car = await _carService.TGetByIdAsync( id );
            if(car == null )
            {
                return NotFound();
            }

            var model = new BookingViewModel
            {
                CarId = car.CarId,
                SelectedCar = car,
                AvailableCars = await _carService.TGetListAsync(),
                Branches = await _branchService.TGetListAsync()


            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.SelectedCar = await _carService.TGetByIdAsync(model.CarId);
                model.AvailableCars =await _carService.TGetListAsync();
                model.Branches = await _branchService.TGetListAsync();
                return View("Index", model);
            }
            var car = await _carService.TGetByIdAsync(model.CarId);
            if(car == null)
            {
                return NotFound();
            }
            var pickUp = model.PickUpDate.Date + TimeSpan.Parse(model.PickUpTime);
            var dropOff = model.DropOffDate.Date + TimeSpan.Parse(model.DropOffTime);

            var days = Math.Max(1, (dropOff.Date - pickUp.Date).Days);
            var totalPrice = days * car.DailyPrice;
            var booking = new Booking
            {
                CarId = model.CarId,
                PickUpBranchId = model.PickUpBranchId,
                DropOffBranchId = model.DropOffBranchId,
                PickUpDate = pickUp,
                DropOffDate = dropOff,
                CustomerName = model.CustomerName,
                CustomerEmail = model.CustomerEmail,
                CustomerPhone = model.CustomerPhone,
                Notes = model.Notes,
                TotalPrice = totalPrice
            };

            await _bookingService.TInsertAsync(booking);

            return RedirectToAction("Confirmation", new { id = booking.BookingId });
        }
    }
}
