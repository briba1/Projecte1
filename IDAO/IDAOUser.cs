using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Projecte1.model;

namespace Projecte1.IDAO
{
    public interface IDAOUser
    {
        public User GetUser(string mail);

        public bool UpdateUser(User u);

        public bool InsertUser(User u);

        public bool DeleteUser(string mail);
    }
}
