﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace vendio_backend.Dtos
{
    public class changePasswordDTO
    {
        [Key]
        public int userId { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Current password")]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("New password")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Confirm new password")]
        public string ConfirmPassword { get; set; }
    }
}

