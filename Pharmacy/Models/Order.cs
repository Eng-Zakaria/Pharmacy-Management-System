using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Models
{
    public class Order
    {
        
        public int Id { get; set; }



        [Required(ErrorMessage = "You have to privde a valid Order Title")]
        public string Title { get; set; }



        public ICollection<Product> Products { get; set; }



        [ValidateNever]
        public int TotalNoProducts { get; set; }



        [ValidateNever]
        public double TotalCost { get; set; }
        

    }
}
