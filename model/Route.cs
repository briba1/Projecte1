using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.Java;
using System.Text;

namespace Projecte1.model
{
    public class Route
    {
        private int idT;
        private string nameT;
        private DateTime createDate;
        private bool validat;
        private int user;
        private int transport;

        public Route(int idT, string nameT, DateTime createDate, bool validat, int user, int transport)
        {
            this.IdT = idT;
            this.NameT = nameT;
            this.CreateDate = createDate;
            this.Validat = validat;
            this.User = user;
            this.Transport = transport;
        }

        public int IdT { get => idT; set => idT = value; }
        public string NameT { get => nameT; set => nameT = value; }
        public DateTime CreateDate { get => createDate; set => createDate = value; }
        public bool Validat { get => validat; set => validat = value; }
        public int User { get => user; set => user = value; }
        public int Transport { get => transport; set => transport = value; }
    }
}