using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Models
{
    public class Purchase
    {
     
        public int Id { get; set; }



        [DataType(DataType.DateTime)]
        [ValidateNever]
        public DateTime PurchaseTime { get; set; }



        [DisplayName("Pharmacist")]
        [Range(1, int.MaxValue, ErrorMessage = "Choose a valid Pharmacist")]
        public int PharmacistId { get; set; }



        [ValidateNever]
        public Pharmacist Pharmacist { get; set; }



        [Range(1, int.MaxValue, ErrorMessage = "Choose a valid Customer")]
        [DisplayName("Customer")]
        public int CustomerId { get; set; }



        [ValidateNever]
        public Customer Customer { get; set; }



        public ICollection<Product> Products { get; set; }



        [ValidateNever]
        public int TotalNoProducts { get; set; }


        [ValidateNever]
        public double TotalCost { get; set; }


    }
}
