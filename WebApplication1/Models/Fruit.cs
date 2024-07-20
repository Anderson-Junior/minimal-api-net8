using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Fruit
    {
        public int id { get; set; }
        public string? name { get; set; }
        public bool instock { get; set; }
    }
}
