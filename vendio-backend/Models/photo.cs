using System;
using System.ComponentModel.DataAnnotations;

namespace datingAppBackend.Models
{
    public class Photo
    {
        [Key]
        public int Id { get; set; }
        [DataType(DataType.Text)]
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public bool isProductPicture { get; set; }
        public int productId { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }
    }
}

