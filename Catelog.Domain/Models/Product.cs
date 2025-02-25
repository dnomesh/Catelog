using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Catelog.Domain.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
        
        public string Code { get; set; }

        [ForeignKey("CategoryId")]
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        
        public DateTime CreationDate { get; set; }

        //Name(string) : Name of the product(e.g., "Laptop," "T-Shirt").
        //Code(string) : Unique code of the product(e.g., "LAPTOP01," "TSHIRT01"). This code must be unique.
        //CategoryCode(string) : Code of the category to which the product belongs.This field establishes the relationship between products and categories.
        //CreationDate(DateTime) : Creation date of the product.
    }
}
