using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LoginRegistration.Models
{
    public class User
    {
        [Key]
        public int UserID {get;set;}
        
        [Required(ErrorMessage = "Must enter a First Name!")]
        public string FirstName {get;set;}
        
        [Required(ErrorMessage = "Must enter a Last Name!")]
        public string LastName {get;set;}
        
        [EmailAddress(ErrorMessage = "Must enter a Valid Email Address!")]
        [Required(ErrorMessage = "Must enter a Valid Email Address!")]
        public string Email {get;set;}

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Must enter a Password!")]
        [MinLength(8, ErrorMessage = "Password must be 8 characters or longer!")]
        public string Password {get;set;}

        [NotMapped]
        [Compare("Password", ErrorMessage = "Passwords must Match!")]
        [DataType(DataType.Password)]
        public string Confirm {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;

    }
}
