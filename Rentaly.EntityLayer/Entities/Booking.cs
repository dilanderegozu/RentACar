using System;
using System.Collections.Generic;
using System.Text;

namespace Rentaly.EntityLayer.Entities
{
    public class Booking
    {
        public int BookingId { get; set; }

        public int CarId { get; set; }
        public Car Car { get; set; }

        public int PickUpBranchId { get; set; }
        public Branch PickUpBranch { get; set; }

        public int DropOffBranchId { get; set; }
        public Branch DropOffBranch { get; set; }

        public DateTime PickUpDate { get; set; }
        public DateTime DropOffDate { get; set; }

        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string? Notes { get; set; }

        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = "Pending";
    }
}
