using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestFood.Models
{
    public class DishModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Info { get; set; }
        public string Picture { get; set; }
        public string Type { get; set; }


    }
}
