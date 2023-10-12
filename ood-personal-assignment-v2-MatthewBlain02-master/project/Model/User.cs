using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace project
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Password { get; set; }
        public bool IsAdmin { get; set; }

        public User(int id, string name, int password, bool isAdmin)
        {
            Id = id;
            Name = name;
            Password = password;
            IsAdmin = isAdmin;
        }
        public User()
        {

        }
        public override string ToString()
        {
            return this.Id + " " + this.Name;
        }
    }
}
