using System;
using System.Collections.Generic;
using System.Text;

namespace Rentaly.EntityLayer.Entities
{
    public class Activity
    {
        public int ActivityId { get; set; }
        public ActivityType Type { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
    public enum ActivityType
    {
        CarAdded,
        CustomerCreated,
        RentalCompleted,
        CarReturned,
        BranchAdded
    }
}
