using FruitApi.Database;
using FruitApi.Models;
using FruitApi.Models.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FruitApi.Repository
{
    public class FruitRepositoryImpl : IFruitRepository
    {
        private readonly FruitDbContext _fruitDbContext;
        public FruitRepositoryImpl(FruitDbContext fruitDbContext)
        {
            _fruitDbContext = fruitDbContext;
        }
        public void CreateFruit(Fruit fruit)
        {
            _fruitDbContext.Add(fruit);
            _fruitDbContext.SaveChanges();
        }

        public void DeleteFruit(string name)
        {
            var fruit= _fruitDbContext.Fruits.FirstOrDefault(x => x.name.ToLower() == name.ToLower());
            if (fruit != null)
            {
                _fruitDbContext.Remove(fruit);
                _fruitDbContext.SaveChanges();
            }
            else
            {
                throw new FruitNotFoundException(name);
            }
        }

        public Fruit GetFruitById(int id)
        {
            var fruit = _fruitDbContext.Fruits.FirstOrDefault(x => x.Id == id);
            if (fruit == null)
                throw new FruitNotFoundException(id);

            return fruit;
        }

        public Fruit? GetFruitByName(string name)
        {
            var fruit = _fruitDbContext.Fruits.Include(f => f.nutritions)
                                              .SingleOrDefault(x => x.name.ToLower() == name.ToLower());

            return fruit;
        }

        public List<Fruit> GetAllFruits()
        {
            return _fruitDbContext.Fruits.Include(f => f.nutritions).ToList();
        }

        public Fruit UpdateFruit(string name, Fruit fruit)
        {
            var fruitForUpdate = _fruitDbContext.Fruits.Include(f => f.nutritions).FirstOrDefault(x => x.name.ToLower() == name.ToLower());
            if (fruitForUpdate == null)
            {
                throw new FruitNotFoundException(name);
            }

            fruitForUpdate.family = fruit.family;
            fruitForUpdate.order = fruit.order;
            fruitForUpdate.genus = fruit.genus;

            if(fruitForUpdate.nutritions != null && fruit.nutritions != null)
            {
                fruitForUpdate.nutritions.calories = fruit.nutritions.calories;
                fruitForUpdate.nutritions.fat = fruit.nutritions.fat;
                fruitForUpdate.nutritions.sugar = fruit.nutritions.sugar;
                fruitForUpdate.nutritions.carbohydrates = fruit.nutritions.carbohydrates;
                fruitForUpdate.nutritions.protein = fruit.nutritions.protein;
            }

            _fruitDbContext.SaveChanges();

            return fruitForUpdate;

        }

     
    }
}
