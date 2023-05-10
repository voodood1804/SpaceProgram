using Bogus.DataSets;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceProgram.Application.models
{
    public enum DangerLevel { Low, Moderate, High, Severe, Critical }
    [Table("SolarSystem")]
    public class SolarSystem
    {
        // neg = negative 
        public SolarSystem(DangerLevel dangerLevel, string name) 
        {
            DangerLevel = dangerLevel;
            Name = name;
        }

        public int Id { get; private set; }
        public DangerLevel DangerLevel { get; set; }
        public string Name { get; set; }
    }
}
