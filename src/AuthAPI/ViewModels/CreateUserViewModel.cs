﻿using System.ComponentModel.DataAnnotations;

namespace AuthAPI.ViewModels
{
    public class CreateUserViewModel
    {
        [Required(ErrorMessage = "The field {0} is required")]
        [MinLength(5, ErrorMessage = "The field {0} must have at least {1} characters")]
        [MaxLength(50, ErrorMessage = "The field {0} must have a maximum of {1} characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The field {0} is required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "The field {0} is in an invalid format")]
        public string Login { get; set; }
        [Required(ErrorMessage = "The field {0} is required")]
        [DataType(DataType.Password, ErrorMessage = "The field {0} is required")]
        [MinLength(6, ErrorMessage = "The field {0} must have at least {1} characters")]
        [MaxLength(20, ErrorMessage = "The field {0} must have a maximum of {1} characters")]
        public string Password { get; set; }
        [Required(ErrorMessage = "The field {0} is required")]
        public string Role { get; set; }
    }
}
