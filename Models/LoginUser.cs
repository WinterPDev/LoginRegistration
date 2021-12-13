using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LoginRegistration.Models
{
    public class LoginUser
    {
        [EmailAddress]
        [Required(ErrorMessage = "Must Enter Email!")]
        public string Email{get; set;}
        [Required(ErrorMessage = "Must Enter Password!")]
        public string Password{get; set;}

    }
}

