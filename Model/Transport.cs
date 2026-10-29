using System;
using System.Collections.Generic;
using System.Text;

namespace Projecte1.model
{
    public class Transport
    {
        private int idM;
        private string nameT;

        public Transport(int idM, string nameT)
        {
            this.NameT = nameT;
            this.IdM = idM;
        }

        public string NameT { get => nameT; set => nameT = value; }
        public int IdM { get => idM; set => idM = value; }
    }
}