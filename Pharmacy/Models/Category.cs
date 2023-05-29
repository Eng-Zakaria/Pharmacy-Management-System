using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Models
{
    public class Category
    {
    
        public int Id { get; set; }



        [Required(ErrorMessage = "You have to privde a valid Category Name")]
        [MaxLength(50, ErrorMessage = "Name mustn't exceed 50 charcters")]
        public string Name { get; set; }



        public string Description { get; set; }



        [ValidateNever]
        public ICollection<Product> Products { get; set; }



        public int NoProducts { get; set; }



    }
}
