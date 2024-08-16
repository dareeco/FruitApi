namespace FruitApi.Models
{
    public class Fruit
    {
        public int Id { get; set; }
        public string name { get; set; } = string.Empty;
        public string? family { get; set; } 
        public string? order { get; set; } = string.Empty;
        public string? genus { get; set; }

        public int NutritionsId { get; set; } 
        public Nutritions? nutritions { get; set; } 

        
    }
}
