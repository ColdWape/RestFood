using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestFood.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public ClientModel Client { get; set; }
        public DishModel Dish { get; set; }

        public string Status { get; set; }

        //public List<DishModel> Dishes { get; set; } = new List<DishModel>();

    }
}
