using System;
namespace vendio_backend.Models
{
    public sealed class PhotoToRegister
    {
        public IFormFile Image { get; set; }
        public int productId { get; set; }        
    }
}

