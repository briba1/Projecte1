using System;
using System.Collections.Generic;
using System.Text;

namespace Projecte1.model
{
    public class Station
    {
        private int idS;
        private string nameS;
        private double latitud;
        private double longitud;
        private int city;

        public Station(int idS, string nameS, double latitud, double longitud, int city)
        {
            this.IdS = idS;
            this.NameS = nameS;
            this.Latitud = latitud;
            this.Longitud = longitud;
            this.City = city;
        }

        public int IdS { get => idS; set => idS = value; }
        public string NameS { get => nameS; set => nameS = value; }
        public double Latitud { get => latitud; set => latitud = value; }
        public double Longitud { get => longitud; set => longitud = value; }
        public int City { get => city; set => city = value; }
    }
}