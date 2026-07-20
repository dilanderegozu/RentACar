using Rentaly.EntityLayer.Entities;
using System.ComponentModel.DataAnnotations;

namespace Rentaly.WebUI.Models
{
    public class BookingViewModel
    {
        public int CarId { get; set; }
        public Car? SelectedCar { get; set; }
        public List<Car> AvailableCars { get; set; } = new();
        public List<Branch> Branches { get; set; } = new();

        public int PickUpBranchId { get; set; }
        public int DropOffBranchId { get; set; }

        public DateTime PickUpDate { get; set; } = DateTime.Today;
        public string PickUpTime { get; set; } = "00:00";

        public DateTime DropOffDate { get; set; } = DateTime.Today;
        public string DropOffTime { get; set; } = "00:00";

        [Required] public string CustomerName { get; set; }
        [Required, EmailAddress] public string CustomerEmail { get; set; }
        [Required, Phone] public string CustomerPhone { get; set; }
        public string? Notes { get; set; }
    }
}
