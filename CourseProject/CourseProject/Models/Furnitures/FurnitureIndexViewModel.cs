using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CourseProject.Models
{
    public class FurnitureIndexViewModel
    {
        public List<Furniture> Furniture { get; set; }

        public PageViewModel PageViewModel { get; set; }

        public List<string> FilterMaterials { get; set; }

        public string FilterMaterial { get; set; }

        public decimal FilterPrice { get; set; }

        public List<int> Ids { get; set; }

        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Материал")]
        public string Material { get; set; }

        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Display(Name = "Количество")]
        public int Count { get; set; }
    }
}
