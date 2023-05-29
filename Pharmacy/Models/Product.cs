using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Models
{
    public class Product
    {
        public int Id { get; set; }



        [Required(ErrorMessage = "You have to privde a valid Product Name")]
        [MaxLength(50, ErrorMessage = "Name mustn't exceed 50 charcters")]
        public string Name { get; set; }



        [DisplayName("Category")]
        [Range(1, int.MaxValue, ErrorMessage = "Choose a valid Category")]
        public int CategoryId {get; set; }



        [ValidateNever]
        public Category Category { get; set; }


        
        public int ProductNoItems { get; set; }



        [ValidateNever]
        [DataType(DataType.DateTime)]
        public DateTime AddedAt { get; set; }



        public string? ActiveSubstance { get; set; }



        public double? Dose { get; set; }



        public string CompanyName { get; set; }



        [DataType(DataType.Date)]
        public DateTime ExpiryDate { get; set; }



        public string? Description { get; set; }



        public double Price { get; set; }



        [DisplayName("Image")]
        [ValidateNever]
        public string ImageUrl { get; set; }



    }
}
