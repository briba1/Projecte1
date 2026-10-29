using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Projecte1.model;

namespace Projecte1.IDAO
{
    public interface IDAOUser
    {
        public ObservableCollection<User> GetEditors(int offset = 0, int rows_per_page = -1);

        public User? GetUser(int id);

        public User? GetUserByMail(string mail);

        public bool InsertUser(User u);

        public bool UpdateUserType(int idU, char uType);
    }
}
