using System;
using System.Collections.Generic;
using System.Text;

namespace Projecte1.model
{
    public class City
    {
        private int idC;
        private string name;

        public City(int idC, string name)
        {
            this.Name = name;
            this.IdC = idC;
        }

        public string Name { get => name; set => name = value; }
        public int IdC { get => idC; set => idC = value; }
    }
}