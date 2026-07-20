using System;
using System.Collections.Generic;
using System.Text;

namespace Rentaly.EntityLayer.Entities
{
    public class CarImage
    {
        public int CarImageId { get; set; }
        public int CarId { get; set; }
        public string ImageUrl { get; set; } = null!;
        public int DisplayOrder { get; set; }
        public bool IsMain { get; set; }

        public Car Car { get; set; } = null!;
    }
}
