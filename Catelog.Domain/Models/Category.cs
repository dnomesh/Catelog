using System.ComponentModel.DataAnnotations;

namespace Catelog.Domain.Models
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public DateTime CreationDate { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();

        //Name(string) : Name of the category(e.g., "Electronics," "Clothing").
        //Code(string) : Unique code of the category(e.g., "ELEC," "CLOTH"). This code must be unique.
        //CreationDate(DateTime) : Creation date of the category.
    }
}
