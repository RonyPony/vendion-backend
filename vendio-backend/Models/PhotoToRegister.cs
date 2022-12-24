using System;
using System.ComponentModel.DataAnnotations;
namespace vendio_backend.Models
{
    public sealed class PhotoToRegister
    {
        [Key]
        public int id { get; set; }
        public IFormFile Image { get; set; }
        public int productId { get; set; }        
    }
}

