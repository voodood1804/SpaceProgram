using Bogus.DataSets;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceProgram.Application.models
{
    [Table("SpaceStation")]
    public class SpaceStation
    {
        public SpaceStation(SolarSystem solarsystem, string name, int longitude, int latitude, int height, int neglongitude, int neglatitude, int negheight) 
        {
            SolarSystem = solarsystem;
            Name = name;
            Longitude = longitude;
            Latitude = latitude;
            Height = height;
            Neglongitude = neglongitude;
            Neglatitude = neglatitude;
            Negheight = negheight;
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        protected SpaceStation() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.


        public int Id { get; private set; }
        public virtual SolarSystem SolarSystem { get; set; }
        public string Name { get; set; }
        public int Longitude { get; set; }
        public int Latitude { get; set; }
        public int Height { get; set; }
        public int Neglongitude { get; set; }
        public int Neglatitude { get; set; }
        public int Negheight { get; set; }
    }
}
