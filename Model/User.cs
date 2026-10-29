using System;
using System.Collections.Generic;
using System.Text;

namespace Projecte1.model
{
    public class User
    {
        private int idU;
        private string mail;
        private string name;
        private string passwd;
        private char uType;
        private DateTime data_reg;

        public User(int idU, string mail, string name, string passwd, char uType, DateTime data_reg)
        {
            this.idU = idU;
            this.Mail = mail;
            this.Name = name;
            this.Passwd = passwd;
            this.UType = uType;
            this.Data_reg = data_reg;
        }

        public int IdU { get => idU; set => idU = value; }
        public string Mail { get => mail; set => mail = value; }
        public string Name { get => name; set => name = value; }
        public string Passwd { get => passwd; set => passwd = value; }
        public char UType { get => uType; set => uType = value; }
        public DateTime Data_reg { get => data_reg; set => data_reg = value; }


    }
}