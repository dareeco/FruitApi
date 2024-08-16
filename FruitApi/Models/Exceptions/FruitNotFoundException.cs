using System;

namespace FruitApi.Models.Exceptions
{
    public class FruitNotFoundException : Exception
    {
        public FruitNotFoundException(string name)
            : base($"Fruit with name '{name}' was not found.")
        {
        }

        public FruitNotFoundException(int id)
            : base($"Fruit with id '{id}' was not found.")
        {
        }
    }
}
