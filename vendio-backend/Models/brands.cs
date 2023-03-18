using System;
using System.ComponentModel.DataAnnotations;

namespace vendio_backend.Controllers
{
	public class CarBrand
	{
        [Key]
        public int Id { get; set; }
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }
        public bool isActive { get; set; }
    }
}

