using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.Java;
using System.Text;

namespace WpfApp1.model
{
    internal class City
    {
        private int idC;
        private string name;

        public City(string name, int idC)
        {
            this.Name = name;
            this.IdC = idC;
        }

        public string Name { get => name; set => name = value; }
        public int IdC { get => idC; set => idC = value; }
    }
}