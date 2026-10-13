using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.Java;
using System.Text;

namespace Projecte1.model
{
    internal class Transport
    {
        private int idM;
        private string nameT;

        public Transport(string nameT, int idM)
        {
            this.NameT = nameT;
            this.IdM = idM;
        }

        public string NameT { get => nameT; set => nameT = value; }
        public int IdM { get => idM; set => idM = value; }
    }
}