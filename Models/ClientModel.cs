using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestFood.Models
{
    public class ClientModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int? RoleId { get; set; }
        public Role Role { get; set; }

    }

    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ClientModel> Users { get; set; }
        public Role()
        {
            Users = new List<ClientModel>();
        }
    }

    public class LogModel
    {
        public string Login { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class RegModel
    {
        public string Login { get; set; }
        public string Name { get; set; }


        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают!")]
        public string AgainPass { get; set; }

    }
}
