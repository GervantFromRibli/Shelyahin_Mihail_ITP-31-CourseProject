using System.ComponentModel.DataAnnotations;

namespace ContextLibrary.Domain_Entities
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string RepresentativeFIO { get; set; }
        public int Number { get; set; }
        public string Address { get; set; }
        public Client() { }
    }
}
