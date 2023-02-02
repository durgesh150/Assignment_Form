using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Assignment_Form.MyValidation;

namespace Assignment_Form.Models
{
    public class User // Model to store all the fields
    {

        [Required(ErrorMessage = "Please enter your first name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your last name")]
        public string LastName { get; set; }
        [Key]
        [Required(ErrorMessage = "Please enter a username")]
        [StringLength(15,MinimumLength =8,ErrorMessage ="UserName should contain 8 to 15 characters only")]
        public string Username { get; set; }//Primaty key fields
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage ="Please select Your Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        [DataType(DataType.Password)]
        [StringLength(25, MinimumLength = 8, ErrorMessage = "Password should contain 8 to 25 characters only")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm your password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [checkValidationAttribute(ErrorMessage = "Please accept terms and conditions")]
        public bool AcceptTerms { get; set; }


    }
}