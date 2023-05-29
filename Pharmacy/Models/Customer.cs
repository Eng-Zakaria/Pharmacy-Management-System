using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Models
{
    public class Customer
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "You have to privde a valid Name")]
        [MaxLength(20, ErrorMessage = "First Name mustn't exceed 20 charcters")]
        [MinLength(2, ErrorMessage = "First Name mustn't be less than 2 charcters")]
        public string Name { get; set; }

        
        public string Address { get; set; }


        public Boolean Gender { get; set; }

        
        public double? Debts { get; set; }


        
        [DisplayName("Phone")]
        [RegularExpression("^0\\d{10}$", ErrorMessage = "Invalid Phone Number!")]
        public int PhoneNo { get; set; }



        [Range(0,150,ErrorMessage ="Please enter valid age between 0,150")]
        public int Age { get; set; }




        [DisplayName("Image")]
        [ValidateNever]
        public string ImageUrl { get; set; }



    }
}
