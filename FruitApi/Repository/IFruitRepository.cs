using FruitApi.Models;

namespace FruitApi.Repository
{
    public interface IFruitRepository
    {
        public List<Fruit> GetAllFruits();
        public Fruit GetFruitById(int id);
        public Fruit? GetFruitByName(string name);
        public Fruit UpdateFruit(string name, Fruit fruit);
        public void CreateFruit(Fruit fruit);
        public void DeleteFruit(string name);
    }
}
