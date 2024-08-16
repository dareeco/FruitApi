using System.Text.Json.Serialization;

namespace FruitApi.Models
{
    public class Nutritions
    {
        [JsonIgnore]  
        public int Id { get; set; }
        public int calories {  get; set; }
        public float fat { get; set; }
        public float sugar {  get; set; }
        public float carbohydrates {  get; set; }
        public float protein {  get; set; }
        [JsonIgnore]  
        public int FruitId { get; set; }  
        [JsonIgnore] 
        public Fruit? Fruit { get; set; }
    }
}
