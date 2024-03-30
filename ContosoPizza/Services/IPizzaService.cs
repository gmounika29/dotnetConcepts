using ContosoPizza.Models;

namespace ContosoPizza.Services
{
    public interface IPizzaService
    {
        void AddPizza(Pizza pizza);
        void DeleteById(int id);
        List<Pizza> GetAll();
        Pizza GetById(int id);
        Pizza update(int id,Pizza pizza);
    }
}
