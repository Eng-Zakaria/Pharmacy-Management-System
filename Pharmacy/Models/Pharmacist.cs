using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Pharmacy.Models
{
    public class Pharmacist
    {

        public int Id { get; set; }


        [DisplayName("First Name")]
        [Required(ErrorMessage = "You have to privde a valid Name")]
        [MaxLength(20, ErrorMessage = "First Name mustn't exceed 20 charcters")]
        [MinLength(2, ErrorMessage = "First Name mustn't be less than 2 charcters")]
        public string FirstName { get; set; }



        [DisplayName("Last Name")]
        [Required(ErrorMessage = "You have to privde a valid Name")]
        [MaxLength(20, ErrorMessage = "First Name mustn't exceed 20 charcters")]
        [MinLength(2, ErrorMessage = "First Name mustn't be less than 2 charcters")]
        public string LastName { get; set; }



        [Range(1200, 15000)]
        public double Salary { get; set; }



        [DisplayName("Added At")]
        [DataType(DataType.DateTime)]
        [ValidateNever]
        public DateTime AddedAt { get; set; }




        [DisplayName("Hiring Date")]
        [DataType(DataType.Date)]
        public DateTime HiringDate { get; set; }


        [DisplayName("Last Updated At")]
        [DataType(DataType.DateTime)]
        [ValidateNever]
        public DateTime LastUpdatedAt { get; set; }



        [DisplayName("Attendance Time")]
        [DataType(DataType.Time)]
        public DateTime Attendance { get; set; }



        [DisplayName("Credit Card")]
        //[RegularExpression("/^(?:4[0-9]{12}(?:[0-9]{3})?|[25][1-7][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\\d{3})\\d{11})$/", ErrorMessage ="Invalid Credit Card")]
        [DataType(DataType.CreditCard)]
        public long CreditCard { get; set; }


        
        
        [DisplayName("Phone Number")]
        //[RegularExpression("^0\\d{10}$", ErrorMessage = "Invalid Phone Number!")]
        [DataType(DataType.PhoneNumber)]
        public int PhoneNo { get; set; }



        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid Email Address!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }



        [DisplayName("Confirm Email")]
        [NotMapped]
        [Compare("Email", ErrorMessage = "Email is not correct")]
        public string ConfirmEmail { get; set; }



        [DataType(DataType.Password)]
        public string Password { get; set; }



        [DisplayName("Confirm Password")]
        [NotMapped]
        [Compare("Password", ErrorMessage = "Password is not correct")]
        public string ConfirmPassword { get; set; }


        
        [Range(18, 65)]
        public int Age { get; set; }



        [DisplayName("Image")]
        [ValidateNever]
        public string ImageUrl { get; set; }



    }
}
