using Rentaly.EntityLayer.Entities;

namespace Rentaly.WebUI.Models
{
    public class CarDetailsViewModel
    {
        public Car Car { get; set; } = null!;
        public List<Branch> Branches { get; set; } = new();
        public List<string> GalleryImages { get; set; } = new(); 
    }
}