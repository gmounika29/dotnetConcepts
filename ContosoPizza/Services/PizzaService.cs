using ContosoPizza.Data;
using ContosoPizza.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ContosoPizza.Services
{
    public class PizzaService : IPizzaService
    {
        public readonly AppDbContext _context;
        
        public PizzaService(AppDbContext context)
        {
            _context = context;
        }

        public List<Pizza> GetAll()
        {
            return _context.Pizzas.ToList();
        }

        public Pizza GetById(int id)
        {
            return _context.Pizzas.Find(id);

        }

        public void DeleteById(int id)
        {
            var pizza = GetById(id);
            if (pizza == null) return;
            _context.Remove(pizza);
            _context.SaveChanges();
        }

        public Pizza update(int id,Pizza pizza)
        {         

            var getPizza = _context.Pizzas.Find(id);
            if (getPizza != null)
            {
                getPizza.Name = pizza.Name;
                getPizza.IsGlutenFree = pizza.IsGlutenFree;

                _context.Pizzas.Update(getPizza);
                _context.SaveChanges();
            }
            return getPizza;

        }

        public void AddPizza(Pizza pizza)
        {

            _context.Pizzas.Add(pizza);
            _context.SaveChanges();
            

        }


    }
}
