using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Pharmacy.Models
{
    public class ProductPurchaseNoItems
    {


        public int Id { get; set; }



        [Range(1, int.MaxValue, ErrorMessage = "Choose a valid Purchase")]
        [DisplayName("Purchase")]
        public int PurchaseId { get; set; }



        public int NoItems { get; set;  }



        [Range(1, int.MaxValue, ErrorMessage = "Choose a valid Product")]
        [DisplayName("Product")]
        public int ProductId { get; set; }




    }
}
