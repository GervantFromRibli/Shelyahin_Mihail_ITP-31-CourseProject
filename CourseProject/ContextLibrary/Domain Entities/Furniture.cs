using System.ComponentModel.DataAnnotations;

namespace ContextLibrary.Domain_Entities
{
    public class Furniture
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Material { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public Furniture() { }
        public Furniture(int id, string name, string descr, string material, decimal price, int count)
        {
            Id = id;
            Name = name;
            Description = descr;
            Material = material;
            Price = price;
            Count = count;
        }
    }
}
